namespace ENTiger.ENCollect.DomainModels.Utilities.ELK_Utilities;

/// <summary>
/// Interface defining Elasticsearch operations and utility methods.
/// </summary>
public interface IElasticSearchService
{


    /// <summary>
    /// Sends a POST request to Elasticsearch with a DSL query.
    /// </summary>
    /// <param name="tenantId">The tenant ID for which the request is being made.</param>
    /// <param name="IndexKeyName">The key name for the specific index.</param>
    /// <param name="dslQuery">The DSL query to be executed in Elasticsearch.</param>
    /// <returns>The JSON response from Elasticsearch as a string.</returns>
    Task<object> SendPostRequestToElasticSearchAsync(string path, string dslQuery);

    /// <summary>
    /// Formats a collection of strings into a comma-separated filter array for Elasticsearch queries.
    /// </summary>
    /// <param name="input">The collection of filter values.</param>
    /// <returns>A formatted string representation of the filter values.</returns>
    string FormatArrayFilter(IEnumerable<string> input);

    /// <summary>
    /// Converts a given input into a valid Elasticsearch filter text.
    /// </summary>
    /// <param name="input">The input filter text.</param>
    /// <returns>A formatted Elasticsearch-compatible filter text.</returns>
    string GetFilterTextForElasticSearch(string input);

    /// <summary>
    /// Adds a valid term clause to a list of clauses if the filter value is not empty or null.
    /// </summary>
    /// <param name="mustClauses">The list of existing must clauses.</param>
    /// <param name="fieldName">The field name to apply the filter on.</param>
    /// <param name="filterValue">The value to be filtered.</param>
    /// <returns>The updated list of must clauses with the term clause added.</returns>
    List<string> AddTermClauseIfValid(List<string> mustClauses, string fieldName, string filterValue);

    /// <summary>
    /// Removes unwanted comma and braces and formats dslquery
    /// </summary>
    /// <param name="dslQuery"></param>
    /// <returns></returns>
    string RemoveCommaFromDslQuery(string dslQuery);
}

