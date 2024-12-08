using System;
using System.Collections.Generic;
using Xunit;
using ToDoDemo.Models;

namespace ToDoDemo.Tests
{
    public class FiltersTests
    {
        [Fact]
        public void Constructor_SetsDefaultFilterString_WhenNullProvided()
        {
            // Arrange: Подготавливаем входные данные - null строка
            string input = null;

            // Act: Создаем объект Filters с null значением
            var filters = new Filters(input);

            // Assert: Проверяем, что значения свойств установлены по умолчанию
            Assert.Equal("all-all-all", filters.FilterString); // Проверка строки фильтра
            Assert.Equal("all", filters.CategoryId);          // Проверка CategoryId
            Assert.Equal("all", filters.Due);                 // Проверка Due
            Assert.Equal("all", filters.StatusId);             // Проверка StatusId
        }

        [Fact]
        public void Constructor_SetsFilterProperties_FromInputString()
        {
            // Arrange: Подготавливаем входные данные - строку с фильтром
            string input = "category1-today-open";

            // Act: Создаем объект Filters с заданной строкой
            var filters = new Filters(input);

            // Assert: Проверяем, что фильтры разобраны правильно
            Assert.Equal("category1-today-open", filters.FilterString);
            Assert.Equal("category1", filters.CategoryId);
            Assert.Equal("today", filters.Due);
            Assert.Equal("open", filters.StatusId);
        }

        [Fact]
        public void HasCategory_ReturnsTrue_WhenCategoryIsSpecified()
        {
            // Arrange: Создаем объект Filters с заданной категорией
            var filters = new Filters("cat1-today-open");

            // Act: Получаем значение свойства HasCategory
            var result = filters.HasCategory;

            // Assert: Проверяем, что категория указана
            Assert.True(result);
        }

        [Fact]
        public void HasCategory_ReturnsFalse_WhenCategoryIsAll()
        {
            // Arrange: Создаем объект Filters с "all" категорией
            var filters = new Filters("all-today-open");

            // Act: Получаем значение свойства HasCategory
            var result = filters.HasCategory;

            // Assert: Проверяем, что категория не указана
            Assert.False(result);
        }

        [Fact]
        public void HasDue_ReturnsTrue_WhenDueIsSpecified()
        {
            // Arrange: Создаем объект Filters с заданной датой выполнения
            var filters = new Filters("cat1-future-open");

            // Act: Получаем значение свойства HasDue
            var result = filters.HasDue;

            // Assert: Проверяем, что дата указана
            Assert.True(result);
        }

        [Fact]
        public void HasDue_ReturnsFalse_WhenDueIsAll()
        {
            // Arrange: Создаем объект Filters с "all" для даты выполнения
            var filters = new Filters("cat1-all-open");

            // Act: Получаем значение свойства HasDue
            var result = filters.HasDue;

            // Assert: Проверяем, что дата не указана
            Assert.False(result);
        }

        [Fact]
        public void HasStatus_ReturnsTrue_WhenStatusIsSpecified()
        {
            // Arrange: Создаем объект Filters с заданным статусом
            var filters = new Filters("cat1-future-closed");

            // Act: Получаем значение свойства HasStatus
            var result = filters.HasStatus;

            // Assert: Проверяем, что статус указан
            Assert.True(result);
        }

        [Fact]
        public void HasStatus_ReturnsFalse_WhenStatusIsAll()
        {
            // Arrange: Создаем объект Filters с "all" статусом
            var filters = new Filters("cat1-future-all");

            // Act: Получаем значение свойства HasStatus
            var result = filters.HasStatus;

            // Assert: Проверяем, что статус не указан
            Assert.False(result);
        }

        [Theory]
        [InlineData("future", true)] // Проверяем, что "future" возвращает true
        [InlineData("past", false)]   // Проверяем, что "past" возвращает false
        [InlineData("today", false)]  // Проверяем, что "today" возвращает false
        public void IsFuture_ReturnsCorrectValue(string due, bool expected)
        {
            // Arrange: Создаем объект Filters с указанной датой выполнения
            var filters = new Filters($"cat1-{due}-open");

            // Act: Получаем значение свойства IsFuture
            var result = filters.IsFuture;

            // Assert: Проверяем, что результат соответствует ожидаемому значению
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("past", true)]    // Проверяем, что "past" возвращает true
        [InlineData("future", false)]  // Проверяем, что "future" возвращает false
        [InlineData("today", false)]   // Проверяем, что "today" возвращает false
        public void IsPast_ReturnsCorrectValue(string due, bool expected)
        {
            // Arrange: Создаем объект Filters с указанной датой выполнения
            var filters = new Filters($"cat1-{due}-open");

            // Act: Получаем значение свойства IsPast
            var result = filters.IsPast;

            // Assert: Проверяем, что результат соответствует ожидаемому значению
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("today", true)]    // Проверяем, что "today" возвращает true
        [InlineData("past", false)]     // Проверяем, что "past" возвращает false
        [InlineData("future", false)]   // Проверяем, что "future" возвращает false
        public void IsToday_ReturnsCorrectValue(string due, bool expected)
        {
            // Arrange: Создаем объект Filters с указанной датой выполнения
            var filters = new Filters($"cat1-{due}-open");

            // Act: Получаем значение свойства IsToday
            var result = filters.IsToday;

            // Assert: Проверяем, что результат соответствует ожидаемому значению
            Assert.Equal(expected, result);
        }
    }
}
