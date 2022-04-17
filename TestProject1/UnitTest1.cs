using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using RestSharp;
using Day37EmployeeManagement;

namespace TestProject1
{

}
[TestClass]
public class RestSharpTestCase
{
    RestClient client;

    [TestInitialize]
    public void Setup()
    {
        client = new RestClient("http://localhost:3000");
    }

    private RestResponse getEmployeeList()
    {
        // Arrange
        // Initialize the request object with proper method and URL
        RestRequest request = new RestRequest("/employees", Method.Get);

        // Act
        // Execute the request
        RestResponse response = client.ExecuteAsync(request).Result;
        return response;
    }

    /* UC1:- Ability to Retrieve all Employees in EmployeePayroll JSON Server.
             - Use JSON Server and RESTSharp to save the EmployeePayroll Data of id, name, and salary.
             - Retrieve in the MSTest Test and corresponding update the Memory with the Data.
   
     */
    [TestMethod]
    public void onCallingGETApi_ReturnEmployeeList()
    {
        RestResponse response = getEmployeeList();

        // Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);     // Comes from using System.Net namespace
        List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
        Assert.AreEqual(7, dataResponse.Count);

        foreach (Employee e in dataResponse)
        {
            System.Console.WriteLine("id: " + e.id + ", Name: " + e.name + ", Salary: " + e.salary);
        }
    }
}