using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using ToDoDemo.Models;

namespace ToDoDemo.Tests
{
    public class ToDoTests
    {
        // Тест, проверяющий, что поле Description является обязательным
        [Fact]
        public void Description_ShouldBeRequired()
        {
            // Arrange: создаем экземпляр ToDo с пустым описанием
            var todo = new ToDo { Description = string.Empty };

            // Act: проверяем валидацию объекта ToDo
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert: объект не должен быть валидным, должно быть одно сообщение об ошибке
            Assert.False(isValid);
            Assert.Single(validationResults);
            Assert.Equal("Add description", validationResults[0].ErrorMessage);
        }

        // Тест, проверяющий, что поле DueDate является обязательным
        [Fact]
        public void DueDate_ShouldBeRequired()
        {
            // Arrange: создаем экземпляр ToDo с отсутствующей датой
            var todo = new ToDo { DueDate = null };

            // Act: проверяем валидацию
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert: объект не должен быть валидным, должно быть одно сообщение об ошибке
            Assert.False(isValid);
            Assert.Single(validationResults);
            Assert.Equal("Add a date", validationResults[0].ErrorMessage);
        }

        // Тест, проверяющий, что поле CategoryId является обязательным
        [Fact]
        public void CategoryId_ShouldBeRequired()
        {
            // Arrange: создаем экземпляр ToDo с пустым CategoryId
            var todo = new ToDo { CategoryId = string.Empty };

            // Act: проверяем валидацию
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert: объект не должен быть валидным, должно быть одно сообщение об ошибке
            Assert.False(isValid);
            Assert.Single(validationResults);
            Assert.Equal("Select a category", validationResults[0].ErrorMessage);
        }

        // Тест, проверяющий, что поле StatusId является обязательным
        [Fact]
        public void StatusId_ShouldBeRequired()
        {
            // Arrange: создаем экземпляр ToDo с пустым StatusId
            var todo = new ToDo { StatusId = string.Empty };

            // Act: проверяем валидацию
            var validationContext = new ValidationContext(todo);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

            // Assert: объект не должен быть валидным, должно быть одно сообщение об ошибке
            Assert.False(isValid);
            Assert.Single(validationResults);
            Assert.Equal("Add status", validationResults[0].ErrorMessage);
        }

        // Тест, проверяющий, что свойство Overdue возвращает true, если статус "open" и дата просрочена
        [Fact]
        public void Overdue_ShouldReturnTrue_WhenStatusIsOpenAndDueDateIsPast()
        {
            // Arrange: создаем ToDo с статусом "open" и просроченной датой
            var todo = new ToDo
            {
                StatusId = "open",
                DueDate = DateTime.Today.AddDays(-1) // По истечении срока
            };

            // Act: проверяем состояние Overdue
            var isOverdue = todo.Overdue;

            // Assert: должно вернуть true
            Assert.True(isOverdue);
        }

        // Тест, проверяющий, что свойство Overdue возвращает false, если статус "open" и дата сегодня
        [Fact]
        public void Overdue_ShouldReturnFalse_WhenStatusIsOpenAndDueDateIsToday()
        {
            // Arrange: создаем ToDo с статусом "open" и датой сегодня
            var todo = new ToDo
            {
                StatusId = "open",
                DueDate = DateTime.Today // Срок истекает сегодня
            };

            // Act: проверяем состояние Overdue
            var isOverdue = todo.Overdue;

            // Assert: должно вернуть false
            Assert.False(isOverdue);
        }

        // Тест, проверяющий, что свойство Overdue возвращает false, если статус "closed"
        [Fact]
        public void Overdue_ShouldReturnFalse_WhenStatusIsClosed()
        {
            // Arrange: создаем ToDo с статусом "closed" и просроченной датой
            var todo = new ToDo
            {
                StatusId = "closed",
                DueDate = DateTime.Today.AddDays(-1) // По истечении срока
            };

            // Act: проверяем состояние Overdue
            var isOverdue = todo.Overdue;

            // Assert: должно вернуть false
            Assert.False(isOverdue);
        }
    }
}
