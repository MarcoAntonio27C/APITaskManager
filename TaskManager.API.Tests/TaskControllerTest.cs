using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.API.Controllers;
using TaskManager.API.Services;

namespace TaskManager.API.Tests
{
    public class TaskControllerTest
    {
        [Fact]
        public void PutStatus_ShouldReturnNotFoundIfTaskDoesNotExist()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();

            mockTaskService.Setup(m => m.UpdateStatus(It.IsAny<Guid>(),It.IsAny<bool>()))
                .Returns((TaskManager.API.Models.Task?)null);

            var controller = new TaskController(mockTaskService.Object);

            // Act
            var result = controller.UpdateStatus(Guid.NewGuid(), true);

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>(result);

        }
    }
}
