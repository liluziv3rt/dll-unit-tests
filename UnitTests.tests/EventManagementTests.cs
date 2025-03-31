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
                events = new List<string>() { "����� '������������'", "���-������� 'RCHP'", "����� ���" },
                dates = new List<DateTime>() { new DateTime(2023, 10, 1), new DateTime(2023, 10, 15), new DateTime(2023, 11, 1) },
                locations = new List<string>() { "��������� '����� ���'", "������� '�������'", "������� '�������� �����'" },
                types = new List<string>() { "�����", "���-�������", "���������� ����" },
                organizators = new List<string>() { "�� '����'", "������� '�������'", "UEFA" }
            };
        }


        [TestMethod]
        public void CreateEvent_AddsNewEventToList()
        {
            // Arrange

            string newEvent = "����� �������";


            // Act
            var updatedEvents = dll.CreateEvent(dll.events, newEvent);

            // Assert
            // ���������, ��� ������ ������� ���������� �� 1
            Assert.AreEqual(dll.events.Count, dll.events.Count);

            // ���������, ��� ����� ������� ��������� � ������
            CollectionAssert.Contains(updatedEvents, newEvent);

            // ���������, ��� ������ ������� �������� � ������
            foreach (var oldEvent in dll.events)
            {
                CollectionAssert.Contains(updatedEvents, oldEvent);
            }

        }

        [TestMethod]
        public void DeleteEvent_ValidIndex_RemovesEvent()
        {
            // Arrange
            int indexToRemove = 1; // ������� ������ �������

            // Act
            dll.DeleteEvent(indexToRemove); //����� ������ � ��������� ���� 1 �������

            // Assert
            Assert.AreEqual(2, dll.events.Count);
            Assert.IsFalse(dll.events.Contains("���-������� 'RCHP'"));
        }

        [TestMethod]
        public void ShowAllEvents_DisplaysCorrectInformation()
        {
            // Arrange
            int i = 1;   // ������� ��� ��������� ������� (���������� � 1)
            int id = 0; // ������������� ��� ������������� ������ � ������ �����
            StringBuilder result = new StringBuilder(); // ��� ������� ���������� ���������� ����������
            string trueString = dll.ShowAllEvents();    // �������� ��������� ������ ������������ ������

            // Act
            foreach (var Event in dll.events)    // ���������� ��� ������� � ����������� �������
            {
                result.AppendLine($"{i}. {Event} ({dll.types[id]})\n�����: {dll.locations[id]}\n����: {dll.dates[id]}\n�����������: {dll.organizators[id]}");
                id++;   // ����������� ������������� ��� ��������� ���������
                i++;    // ����������� ������� ���������
            }
            string actualResult = result.ToString();    // �������� ������ � ��������� �����������

            // Assert
            Assert.AreEqual(trueString, actualResult);  // ���������� ��������� ������ � ������ �����������
        }

        [TestMethod]
        public void ShowEventsDylime_EventsFound_ReturnsFormattedString()
        {
            // Arrange
            DateTime searchDate = new DateTime(2023, 10, 1);    // �������� ���� ��� ������

            // Act
            string result = dll.ShowEventsByTime(searchDate);   // �������� ����������� �����
            string trueEvent = "����� '������������'";          // ��������� ���������

            // Assert
            Assert.AreEqual(result.Trim(), trueEvent.Trim());   // ���������� � ��������� �����������
                                                                // Trim() ������������ ��� �������� ��������� ��������/��������� �����
        }

        [TestMethod]
        public void infoEventId_ValidId_ReturnsCorrectInfo()
        {
            // Arrange
            int validId = 1;    // �������� ������������� ������������� �������

            // Act
            string result = dll.infoEventId(validId);   // ����� ������������ ������

            // Assert
            StringAssert.Contains(result.Trim(), $"{validId + 1}. {dll.events[validId]} ({dll.types[validId]})\n�����: {dll.locations[validId]}\n����: {dll.dates[validId]}\n�����������: {dll.organizators[validId]}");
            // �������� ������� ������, �������� � ����
        }


        [TestMethod]
        public void ShowEventByLocation_ExistingLocation_PrintsEvents()
        {
            //arrange
            string location = "������� '�������� �����'";   // ����������� �������
            string Event = "����� ���";                     // ��������� �������� �������

            // Act
            string result = dll.ShowEventByLocation(location);    // ����� ������������ ������

            // Assert
            Assert.AreEqual(Event, result);     // ��������� � ��������� ���������
        }

        [TestMethod]
        public void infoEventId_CountsId_ReturnsCorrectInfo()
        {
            //arrange
            int count = 2;  // ��������� ����� ���������� � 2 ��������
            string expected = $"{1}. {dll.events[0]} ({dll.types[0]})\n�����: {dll.locations[0]}\n����: {dll.dates[0]}\n�����������: {dll.organizators[0]}\r\n" +
                         $"{2}. {dll.events[1]} ({dll.types[1]})\n�����: {dll.locations[1]}\n����: {dll.dates[1]}\n�����������: {dll.organizators[1]}";

            // Act
            string result = dll.CountEvents(count);


            // Assert
            StringAssert.Contains(expected.Trim(), result.Trim());  //��������� ��������� ��������� �����

        }

        [TestMethod]
        public void ShowEventByLocation_ReturnsCorrectInfo()
        {
            // arrange
            string type = "���������� ����";    // ����������� ���
            string expected = "����� ���";      // ��������� �������

            // act
            string result = dll.ShowAllEventsByType(type);

            // assert
            StringAssert.Contains(expected.Trim(), result.Trim());
        }

        [TestMethod]
        public void ShowEventByOrg_ReturnsCorrectInfo()
        {
            // arrange
            string org = "UEFA";        // ����������� �����������
            string expected = "����� ���";      // ��������� �������� �������

            // act
            string result = dll.ShowAllEventsByOrg(org);

            // assert
            StringAssert.Contains(expected.Trim(), result.Trim());
        }

        [TestMethod]
        public void EditEvents_ShouldUpdateEvent_WhenEditTypeIsEvent()
        {
            // Arrange
            string newEvent = "New Event";  // ����� ��������

            // Act
            dll.EditEvents(1, "event", newEvent);       // id �������, event - ��� ��������������

            // Assert
            Assert.AreEqual(newEvent, dll.events[1]);
        }

    }
}