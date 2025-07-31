using ENTiger.ENCollect.CompanyUsersModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// Service responsible for processing geo-tag report generation logic.
    /// </summary>
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// Generates a geo report based on the provided DTO and triggers the associated command.
        /// </summary>
        /// <param name="dto">The input data transfer object containing report parameters.</param>
        /// <returns>A <see cref="CommandResult"/> indicating the outcome of the operation.</returns>
        public async Task<CommandResult> GenerateGeoReport(GeoCannedReportDto dto)
        {
            // Assign a unique ID for tracking
            dto.SetGeneratedId(_pkGenerator.GenerateKey());

            // Prepare and send the command
            var command = new GenerateGeoReportCommand
            {
                Dto = dto
            };

            await ProcessCommand(command);

            // Return a standardized success result
            return new CommandResult(Status.Success)
            {
                result = new GenerateGeoReportResultModel()
            };
        }
    }

    /// <summary>
    /// Placeholder model for geo report generation output.
    /// Extend this class to return custom result data in the future.
    /// </summary>
    public class GenerateGeoReportResultModel : DtoBridge
    {
    }
}
