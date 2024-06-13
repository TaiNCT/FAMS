using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ScoreManagementAPI.Controllers;
using ScoreManagementAPI.DTO;
using ScoreManagementAPI.Interfaces;
using ScoreManagementAPI.Repository;
using System.Text.Json;


namespace ScoreManagementAPITesting.certControllerTest
{
    public class certControllerTest
    {
        private readonly ICertRepository _repository;
        private readonly IMapper _mapper;
        public certControllerTest()
        {
            _repository = A.Fake<ICertRepository>();
            _mapper = A.Fake<IMapper>();
        }


        // List data of a user certificate
        [Fact]
        public async Task certController_GetCertAwardAsync_ExpectedID()
        {
            // Create a CertRequest object
            var certRequest = new CertRequest
            {
                studentid = "7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50",
                // Set other properties as needed
            };

            // Call GetCertAwardAsync with the CertRequest object
            var ret = await (new certController(_repository)).GetCertAwardAsync(certRequest);

            // Assert
            Assert.NotNull(ret);
            var result = Assert.IsType<OkObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(200, result.StatusCode); // Ensure that the status code is 200
        }

        [Fact]
        public async Task certController_GetCertAwardAsync_UnexpectedID()
        {
            // Create a CertRequest object
            var certRequest = new CertRequest
            {
                studentid = "asdfghyujiopqwerty6789azsdfyuio0psdrftyuiop",
                // Set other properties as needed
            };

            // Call GetCertAwardAsync with the CertRequest object
            var ret = await (new certController(_repository)).GetCertAwardAsync(certRequest);

            // Assert
            Assert.NotNull(ret);
            var result = Assert.IsType<OkObjectResult>(ret); // Ensure that the action returned a NotFoundObjectResult
            Assert.Equal(200, result.StatusCode); // Ensure that the status code is 404
        }

        // Update certificate
        [Fact]
        public async Task certController_UpdateCertAwardAsync_ExpectedData()
        {
            // Response
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(A.Fake<FormatJSON>());

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
            Assert.Equal(JsonSerializer.Serialize(new { msg = $"No student with ID=\"\" found." }), JsonSerializer.Serialize(notfoundret.Value)); // Make sure the return object contain a key as "msg

        }
        [Fact]
        public async Task certController_UpdateCertAwardAsync_UnexpectedData()
        {
            // Response
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(A.Fake<FormatJSON>());

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
            Assert.Equal(JsonSerializer.Serialize(new { msg = $"No student with ID=\"\" found." }), JsonSerializer.Serialize(notfoundret.Value)); // Make sure the return object contain a key as "msg

        }
        [Fact]
        public async Task certController_UpdateCertAwardAsync_EmptyData()
        {
            // Response
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(A.Fake<FormatJSON>());

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
            Assert.Equal(JsonSerializer.Serialize(new { msg = $"No student with ID=\"\" found." }), JsonSerializer.Serialize(notfoundret.Value)); // Make sure the return object contain a key as "msg

        }
        [Fact]
        public async Task certController_UpdateCertAwardAsync_SQLi_1()
        {
            // Response
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(A.Fake<FormatJSON>());

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
            Assert.Equal(JsonSerializer.Serialize(new { msg = $"No student with ID=\"\" found." }), JsonSerializer.Serialize(notfoundret.Value)); // Make sure the return object contain a key as "msg

        }

        [Fact]
        public async Task certController_UpdateCertAwardAsync_SQLi_2()
        {
            // Response
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(A.Fake<FormatJSON>());

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
            Assert.Equal(JsonSerializer.Serialize(new { msg = $"No student with ID=\"\" found." }), JsonSerializer.Serialize(notfoundret.Value)); // Make sure the return object contain a key as "msg

        }

        [Fact]
        public async Task certController_UpdateCertAwardAsync_ValidPhone()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.phone = "0992-1222-091";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
            Assert.Equal(JsonSerializer.Serialize(new { msg = $"No student with ID=\"\" found." }), JsonSerializer.Serialize(notfoundret.Value)); // Make sure the return object contain a key as "msg
        }
        [Fact]
        public async Task certController_UpdateCertAwardAsync_MalformedPhone_1()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.phone = "@#$%^&*(";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
        }

        [Fact]
        public async Task certController_UpdateCertAwardAsync_MalformedPhone_2()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.phone = "111-1111-8201";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
        }


        [Fact]
        public async Task certController_UpdateCertAwardAsync_MalformedGender()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.phone = "Females";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
        }

        [Fact]
        public async Task certController_UpdateCertAwardAsync_MalformedEmail_1()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.email = "234567890-=DFGHUJI#@$%^&*()_";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
        }
        [Fact]
        public async Task certController_UpdateCertAwardAsync_MalformedEmail_2()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.email = "vinhdtgcs200644@";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<NotFoundObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(404, notfoundret.StatusCode); // Ensure that the status code is 404
        }

        public async Task certController_UpdateCertAwardAsync_MalformedLocation_1()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.permanentResidence = "Hồ CHí Minh city";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<BadRequestObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(400, notfoundret.StatusCode); // Ensure that the status code is 404
        }

        public async Task certController_UpdateCertAwardAsync_MalformedLocation_2()
        {
            // Response
            var data = A.Fake<FormatJSON>();
            data.permanentResidence = "Hồ CHí Miawdawdwdnh city";
            var ret = await (new certController(_repository)).UpdateCertAwardAsync(data);

            // Assert
            Assert.NotNull(ret);
            var notfoundret = Assert.IsType<BadRequestObjectResult>(ret); // Ensure that the action returned an OkObjectResult
            Assert.Equal(400, notfoundret.StatusCode); // Ensure that the status code is 404
        }
    }
}
