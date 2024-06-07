using NUnit.Framework;
using TaskManager.Core;
using TaskManager.Core.Abstractions;
using TaskManager.DataAccess.Repositories;
using TaskManager.Tests.fixtures;

namespace TaskManager.Tests;

public class TaskRepositoryTests
{
    private ITaskRepository _taskRepository;


    private static MyTask GetMyTask()
    {
        return MyTask.Create(
            Guid.NewGuid(),
            "MyTask",
            "test",
            MyTaskStatus.Completed,
            DateTime.Now,
            DateTime.Now,
            Guid.NewGuid()).MyTask;
    }

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        TaskManagerFixture.OneTimeSetUp();
    }

    [SetUp]
    public void Setup()
    {
        _taskRepository = new TasksRepository(TaskManagerFixture.context);
    }

    [Test]
    public async Task Create()
    {
        var newMyTask = GetMyTask();

        var result = await _taskRepository.Create(newMyTask);

        Assert.That(result, Is.EqualTo(newMyTask.Id));
    }

    [Test]
    public async Task GetTask()
    {
        var newMyTask = GetMyTask();
        await _taskRepository.Create(newMyTask);

        var result = await _taskRepository.Get(newMyTask.Id);

        Assert.That(result.myTask.Id, Is.EqualTo(newMyTask.Id));
        Assert.That(result.myTask.UserId, Is.EqualTo(newMyTask.UserId));
        Assert.That(result.myTask.Description, Is.EqualTo(newMyTask.Description));
        Assert.That(result.myTask.Status, Is.EqualTo(newMyTask.Status));
        Assert.That(result.myTask.Title, Is.EqualTo(newMyTask.Title));
        Assert.That(result.myTask.DueDate, Is.EqualTo(newMyTask.DueDate));
        Assert.That(result.myTask.CreatedDate, Is.EqualTo(newMyTask.CreatedDate));
    }
}