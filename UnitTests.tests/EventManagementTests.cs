using EventManagement;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTests.tests
{
    [TestClass]
    public class UnitTest
    {
        public EventManagementDll dll;

        [TestInitialize]
        public void Setup()
        {
            dll = new EventManagementDll
            {
                events = new List<string>() { "Фильм 'Трансформеры'", "Рок-Концерт 'RCHP'", "Финал АПЛ" },
                dates = new List<DateTime>() { new DateTime(2023, 10, 1), new DateTime(2023, 10, 15), new DateTime(2023, 11, 1) },
                locations = new List<string>() { "Кинотеатр 'Новая эра'", "Стадион 'Лужники'", "Стадион 'Стенфорд Бридж'" },
                types = new List<string>() { "Фильм", "Рок-концерт", "Футбольный матч" },
                organizators = new List<string>() { "ТЦ 'Небо'", "Стадион 'Лужники'", "UEFA" }
            };
        }


        [TestMethod]
        public void CreateEvent_AddsNewEventToList()
        {
            // Arrange

            string newEvent = "Новое событие";


            // Act
            var updatedEvents = dll.CreateEvent(dll.events, newEvent);

            // Assert
            // Проверяем, что список событий увеличился на 1
            Assert.AreEqual(dll.events.Count, dll.events.Count);

            // Проверяем, что новое событие добавлено в список
            CollectionAssert.Contains(updatedEvents, newEvent);

            // Проверяем, что старые события остались в списке
            foreach (var oldEvent in dll.events)
            {
                CollectionAssert.Contains(updatedEvents, oldEvent);
            }

        }

        [TestMethod]
        public void DeleteEvent_ValidIndex_RemovesEvent()
        {
            // Arrange
            int indexToRemove = 1; // Удаляем второе событие

            // Act
            dll.DeleteEvent(indexToRemove); //Вызов метода с передачей туда 1 индекса

            // Assert
            Assert.AreEqual(2, dll.events.Count);
            Assert.IsFalse(dll.events.Contains("Рок-Концерт 'RCHP'"));
        }

        [TestMethod]
        public void ShowAllEvents_DisplaysCorrectInformation()
        {
            // Arrange
            int i = 1;   // Счетчик для нумерации событий (начинается с 1)
            int id = 0; // Идентификатор для сопоставления данных в разных списк
            StringBuilder result = new StringBuilder(); // Для ручного построения ожидаемого результата
            string trueString = dll.ShowAllEvents();    // Получаем результат работы тестируемого метода

            // Act
            foreach (var Event in dll.events)    // Перебираем все события в тестируемом объекте
            {
                result.AppendLine($"{i}. {Event} ({dll.types[id]})\nМесто: {dll.locations[id]}\nДата: {dll.dates[id]}\nОрганизатор: {dll.organizators[id]}");
                id++;   // Увеличиваем идентификатор для следующих элементов
                i++;    // Увеличиваем счетчик нумерации
            }
            string actualResult = result.ToString();    // Получаем строку с ожидаемым результатом

            // Assert
            Assert.AreEqual(trueString, actualResult);  // Сравниваем результат метода с ручным построением
        }

        [TestMethod]
        public void ShowEventsDylime_EventsFound_ReturnsFormattedString()
        {
            // Arrange
            DateTime searchDate = new DateTime(2023, 10, 1);    // Тестовая дата для поиска

            // Act
            string result = dll.ShowEventsByTime(searchDate);   // Вызываем тестируемый метод
            string trueEvent = "Фильм 'Трансформеры'";          // Ожидаемый результат

            // Assert
            Assert.AreEqual(result.Trim(), trueEvent.Trim());   // Сравниваем с ожидаемым результатом
                                                                // Trim() используется для удаления возможных пробелов/переносов строк
        }

        [TestMethod]
        public void infoEventId_ValidId_ReturnsCorrectInfo()
        {
            // Arrange
            int validId = 1;    // Валидный идентификатор существующего события

            // Act
            string result = dll.infoEventId(validId);   // Вызов тестируемого метода

            // Assert
            StringAssert.Contains(result.Trim(), $"{validId + 1}. {dll.events[validId]} ({dll.types[validId]})\nМесто: {dll.locations[validId]}\nДата: {dll.dates[validId]}\nОрганизатор: {dll.organizators[validId]}");
            // Проверка наличия номера, названия и типа
        }


        [TestMethod]
        public void ShowEventByLocation_ExistingLocation_PrintsEvents()
        {
            //arrange
            string location = "Стадион 'Стенфорд Бридж'";   // Фактическая локация
            string Event = "Финал АПЛ";                     // Ожидаемое название события

            // Act
            string result = dll.ShowEventByLocation(location);    // Вызов тестируемого метода

            // Assert
            Assert.AreEqual(Event, result);     // Сравнение с ожидаемым значением
        }

        [TestMethod]
        public void infoEventId_CountsId_ReturnsCorrectInfo()
        {
            //arrange
            int count = 2;  // Проверяем вывод информации о 2 событиях
            string expected = $"{1}. {dll.events[0]} ({dll.types[0]})\nМесто: {dll.locations[0]}\nДата: {dll.dates[0]}\nОрганизатор: {dll.organizators[0]}\r\n" +
                         $"{2}. {dll.events[1]} ({dll.types[1]})\nМесто: {dll.locations[1]}\nДата: {dll.dates[1]}\nОрганизатор: {dll.organizators[1]}";

            // Act
            string result = dll.CountEvents(count);


            // Assert
            StringAssert.Contains(expected.Trim(), result.Trim());  //Проверяем частичное совпдание строк

        }

        [TestMethod]
        public void ShowEventByLocation_ReturnsCorrectInfo()
        {
            // arrange
            string type = "Футбольный матч";    // Фактический тип
            string expected = "Финал АПЛ";      // Ожидаемое событие

            // act
            string result = dll.ShowAllEventsByType(type);

            // assert
            StringAssert.Contains(expected.Trim(), result.Trim());
        }

        [TestMethod]
        public void ShowEventByOrg_ReturnsCorrectInfo()
        {
            // arrange
            string org = "UEFA";        // Фактический организатор
            string expected = "Финал АПЛ";      // Ожидаемое название события

            // act
            string result = dll.ShowAllEventsByOrg(org);

            // assert
            StringAssert.Contains(expected.Trim(), result.Trim());
        }

        [TestMethod]
        public void EditEvents_ShouldUpdateEvent_WhenEditTypeIsEvent()
        {
            // Arrange
            string newEvent = "New Event";  // Новое название

            // Act
            dll.EditEvents(1, "event", newEvent);       // id события, event - Тип редактирования

            // Assert
            Assert.AreEqual(newEvent, dll.events[1]);
        }

    }
}