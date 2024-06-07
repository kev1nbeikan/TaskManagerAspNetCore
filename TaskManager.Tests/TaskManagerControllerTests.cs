using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TaskManager.Core;
using TaskManager.Core.Abstractions;
using WebApplication3.Contracts;
using WebApplication3.Controllers;


namespace TaskManager.Tests;

public class TaskManagerControllerTests
{
    private ControllerContext _taskControllerContext;


    public TaskManagerControllerTests()
    {
    }

    [SetUp]
    public void SetupTaskControllerContext()
    {
        var userId = Guid.NewGuid();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(UserClaims.UserId, userId.ToString())
        }));


        _taskControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = user
            }
        };
    }


    [Test]
    public async Task GetTask()
    {
        var taskService = new Mock<ITaskService>();
        var logger = new Mock<ILogger<TaskController>>();
        var id = Guid.NewGuid();

        taskService.Setup(service => service.CreateTask(It.IsAny<MyTask>())).ReturnsAsync(id);


        var taskController = new TaskController(taskService.Object, logger.Object)
            { ControllerContext = _taskControllerContext };
        
        var taskRequest = new TaskRequest("title", "description", MyTaskStatus.InProgress, DateTime.Now);

        var result = await taskController.CreateTask(taskRequest);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.EqualTo(id));
    }
}