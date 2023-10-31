using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Mvc;

namespace TestGoogleDriveProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoogleSheetController : ControllerBase
    {

        private readonly ILogger<GoogleSheetController> _logger;
        private readonly IConfiguration _configuration;


        public GoogleSheetController(ILogger<GoogleSheetController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }

        [HttpGet(Name = "GetContentFromGoogleSheet")]
        public ActionResult<IEnumerable<IEnumerable<string>>> Get([FromQuery] string columnRangeBegin = "A", [FromQuery] string columnRangeEnd = "Z")
        {
            try
            {
                // Get the user secrets configs to access the Google Sheet API
                // these are the basic resources we will need to access any sheet
                var privateKey = _configuration["GoogleSheetsAPIKey"];
                var serviceAccountEmail = _configuration["GoogleSheetsAPIServiceAccountEmail"];
                var spreadsheetId = _configuration["GoogleSheetsAPIFileId"];
                var sheetName = _configuration["GoogleSheetsAPISheetName"];



                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = new[] { SheetsService.Scope.Spreadsheets },
                    User = serviceAccountEmail
                }.FromPrivateKey(privateKey));

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential
                });

                // Range of the data you want to retrieve, e.g., "SHEETNAME!COLUMN?ROW:COLUMN?ROW"
                // the ROW parameter is optional, and it will grab all rows if not specified
                string range = $"{sheetName}!{columnRangeBegin.ToUpper()}:{columnRangeEnd.ToUpper()}";

                // Make the request to the Sheets API
                SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
                ValueRange response = request.Execute();
                IList<IList<object>> values = response.Values;

                // Check if data was retrieved
                if (values != null && values.Count > 0)
                {
                    // Retrieve and return the first row
                    return Ok(values);
                }
                else
                {
                    return BadRequest("No data found in the specified range.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
