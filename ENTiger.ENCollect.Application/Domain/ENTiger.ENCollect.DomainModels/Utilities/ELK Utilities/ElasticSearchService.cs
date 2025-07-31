using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.DomainModels.Utilities.ELK_Utilities;

/// <summary>
/// Service to interact with Elasticsearch.
/// </summary>
public class ElasticSearchService : IElasticSearchService
{
    private readonly ElasticsearchClient _client;
    protected readonly ILogger<ElasticSearchService> _logger;

    /// <summary>
    /// If the input is null,blank and 'ALL' then to construct dslquery we are considering it as no filter
    /// </summary>
    public const string NoFilterPresent = "NoFilterPresent";


    /// <summary>
    /// Constructor for initializing Elasticsearch service.
    /// </summary>
    public ElasticSearchService(ElasticsearchClient client, ILogger<ElasticSearchService> logger)
    {
        Guard.AgainstNull(nameof(client), client);
        Guard.AgainstNull(nameof(logger), logger);

        _client = client;
        _logger = logger;
    }


    /// <summary>
    /// Sends a POST request to Elasticsearch with a DSL query.
    /// </summary>
    public async Task<object> SendPostRequestToElasticSearchAsync(string path, string dslQuery)
    {

        Guard.AgainstNullAndEmpty("path", path);
        Guard.AgainstNullAndEmpty("dslQuery", dslQuery);
        string responseBody = string.Empty;

        try
        {
            var response = await _client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, path, PostData.String(dslQuery));

            var statusCode = response.ApiCallDetails.HttpStatusCode;

            _logger.LogInformation($"SendPostRequestToElasticSearch StatusCode : {statusCode}  ");

            if (statusCode != 200)
            {
                return HandleErrorResponse(response.Body);
            }
            else
            {

                return response.Body;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while communicating with Elasticsearch." + ex);
            responseBody = "";
            return responseBody;

        }

    }

    /// <summary>
    /// Formats an array filter for Elasticsearch.
    /// </summary>
    public string FormatArrayFilter(IEnumerable<string> input)
    {
        return input != null && input.Any()
                    ? string.Join(",", input.Select(value => $"\"{GetFilterTextForElasticSearch(value)}\""))
                    : string.Empty;
    }

    /// <summary>
    /// Prepares a filter text for Elasticsearch queries.
    /// </summary>
    public string GetFilterTextForElasticSearch(string input)
    {
        if (input is null)
            return NoFilterPresent;

        if (string.IsNullOrWhiteSpace(input) || input.Equals("ALL", StringComparison.OrdinalIgnoreCase))
            return NoFilterPresent;

        input = input.Trim().ToUpper();

        if (input.Contains("*"))
            return input.Replace("*", "\\\"*\\\"");

        return input.Equals("BLANK", StringComparison.OrdinalIgnoreCase) ? string.Empty : input;

    }

    /// <summary>
    /// Adds a term clause to the query if the filter value is valid.
    /// </summary>
    public List<string> AddTermClauseIfValid(List<string> mustClauses, string fieldName, string filterValue)
    {
        if (!string.IsNullOrEmpty(filterValue) && filterValue != MagickString.NoFilterPresent)
        {
            mustClauses.Add($@"{{ ""term"": {{ ""{fieldName}"": {{ ""value"": ""{filterValue}"" }} }} }}");
        }

        return mustClauses;
    }
    /// <summary>
    /// Handles error response from Elasticsearch and logs details.
    /// </summary>
    private string HandleErrorResponse(string? responseBody)
    {
        Guard.AgainstNullAndEmpty(nameof(responseBody), responseBody);
        try
        {
            using var jsonDoc = JsonDocument.Parse(responseBody);
            if (jsonDoc.RootElement.TryGetProperty("error", out var errorElement) &&
                errorElement.TryGetProperty("root_cause", out var rootCauseArray))
            {
                foreach (var error in rootCauseArray.EnumerateArray())
                {
                    _logger.LogError("Elasticsearch Error: {ErrorMessage}", error.ToString());
                }
            }
        }
        catch (JsonException jsonException)
        {
            _logger.LogError("Failed to parse Elasticsearch error response {jsonException}.", jsonException);
        }

        return string.Empty;
    }

    public string RemoveCommaFromDslQuery(string dslQuery)
    {
        string replaceWith = "";
        string removedBreaks = dslQuery.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

        removedBreaks = Regex.Replace(removedBreaks, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");

        removedBreaks = removedBreaks.Replace(",]", "]");
        removedBreaks = removedBreaks.Replace(",}", "}");
        removedBreaks = removedBreaks.Replace(",)", ")");
        removedBreaks = removedBreaks.Replace(",,", ",");

        dslQuery = removedBreaks;
        return dslQuery;
    }
}
