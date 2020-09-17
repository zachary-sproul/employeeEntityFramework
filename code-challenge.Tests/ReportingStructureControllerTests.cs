using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        /*
        Create a new type, ReportingStructure, that has two properties: employee and numberOfReports.

        For the field "numberOfReports", this should equal the total number of reports under a given employee.The number of reports is determined to be the number of directReports for an employee and all of their direct reports.For example, given the following employee structure:
        ```
                            John Lennon
                        /               \
                 Paul McCartney         Ringo Starr
                                       /        \
                                  Pete Best     George Harrison
        ```
        The numberOfReports for employee John Lennon (employeeId: 16a596ae-edd3-4847-99fe-c4518e82c86f) would be equal to 4. 

        This new type should have a new REST endpoint created for it.This new endpoint should accept an employeeId and return the fully filled out ReportingStructure for the specified employeeId.The values should be computed on the fly and will not be persisted.
        */
        [TestMethod]
        [DataRow("16a596ae-edd3-4847-99fe-c4518e82c86f", "Engineering", "John", "Lennon", "Development Manager", 4, 
            DisplayName = "GetReportingStructure_Returns_DirectReports_MultiLevel")]
        [DataRow("03aa1462-ffa9-4978-901b-7c001562cf6f", "Engineering", "Ringo", "Starr", "Developer V", 2, 
            DisplayName = "GetReportingStructure_Returns_DirectReports_SingleLevel")]
        [DataRow("62c1084e-6e34-4630-93fd-9153afb65309", "Engineering", "Pete", "Best", "Developer II", 0, 
            DisplayName = "GetReportingStructure_Returns_DirectReports_None")]
        public void GetReportingStructure_Returns_DirectReports(string employeeId, string expectedDepartment, string expectedFirstName, string expectedLastName, string expectedPosition, int expectedNumberOfReports)
        {
            // Arrange
            //everything is passed in

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.IsNotNull(reportingStructure.Employee);
            Assert.AreEqual(employeeId, reportingStructure.Employee.EmployeeId);
            Assert.AreEqual(expectedFirstName, reportingStructure.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.Employee.LastName);
            Assert.AreEqual(expectedDepartment, reportingStructure.Employee.Department);
            Assert.AreEqual(expectedPosition, reportingStructure.Employee.Position);
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.NumberOfReports);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_NotFound()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "32c1084e-6e34-4630-39fd-9153afb65309",
                Department = "Complaints",
                FirstName = "Debbie",
                LastName = "Downer",
                Position = "Receiver",
            };

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employee.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();

            Assert.IsNull(reportingStructure.Employee);
        }
    }
}
