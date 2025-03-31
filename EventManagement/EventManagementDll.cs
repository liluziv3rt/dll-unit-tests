using System;
using System.Globalization;
using System.Text;

namespace EventManagement
{
    public class EventManagementDll
    {
        //Коллекции, с которыми будет работать пользователь (они заранее заполнены)
        public List<string> events { get; set; }
        public List<string> types { get; set; }
        public List<string> locations { get; set; }
        public List<DateTime> dates { get; set; }
        public List<string> organizators { get; set; }

        //Создает новое событие и добавляет его в коллекцию
        public List<string> CreateEvent( List<string> events, string Event)
        {
            if (Event != null) //Если передано null в качестве события, добавления не происходит
            {
                events.Add(Event);
            }
            return events;
        }

        //Удаляет событие по указанному индексу
        public void DeleteEvent(int indexToRemove)
        {
            if (indexToRemove != null) //Если индекс равен null, удаление не производится
            {
                events.RemoveAt(indexToRemove);
            }
            else return;
        }

        //Метод для показа всех событий
        public string ShowAllEvents()
        {
            StringBuilder result = new StringBuilder();  // Создаем объект для эффективного построения строки
            int i = 1;  // Счетчик для нумерации событий (начинается с 1)
            int id = 0;  // ID события для сопоставления с другими списками (типы, локации и т.д.)
            foreach (var Event in events)   // Перебираем все события в списке events
            {
                result.AppendLine($"{i}. {Event} ({types[id]})\nМесто: {locations[id]}\nДата: {dates[id]}\nОрганизатор: {organizators[id]}"); //Добавление строки в переменуню result
                id++;   // Увеличиваем id для обращения к следующим элементам в сопутствующих списках
                i++;  // Увеличиваем счетчик нумерации
            }
            return result.ToString();
        }

        // Метод для отображения событий, происходящих в указанную дату
        public string ShowEventsByTime(DateTime needDate)
        {
            StringBuilder result = new StringBuilder();
            int i = 1;
            bool flag = false;    // Флаг для определения, были ли найдены события
            int id = 0;
            foreach (var Event in events)
            {
                if (needDate == dates[id])  // Проверяем, совпадает ли дата события с искомой датой
                {
                    result.AppendLine($"{Event}");   // Если дата совпадает, добавляем название события в результат
                    flag = true;    // Устанавливаем флаг, что события найдены
                }
                id++;
                i++;
            }
            if (flag == false) return "None";    // Если события не найдены (флаг остался false), возвращаем "None"
            else return result.ToString();  // Иначе возвращаем строку с найденными событиями

        }

        //Метод для показа события по введенному id
        public string infoEventId(int id)
        {
            StringBuilder result = new StringBuilder();// Проверяем, что переданный id находится в допустимых границах:
            if (id<0 || id>events.Count)               // не может быть отрицательным и не может превышать количество событий
            {
                throw new ArgumentException("Введеный id превышает количество событий.");   // Если проверка не пройдена - выбрасываем исключение с сообщением об ошибке
            }
            result.AppendLine($"{id+1}. {events[id]} ({types[id]})\nМесто: {locations[id]}\nДата: {dates[id]}\nОрганизатор: {organizators[id]}");
            return result.ToString();
        }

        // Метод для поиска и отображения события по месту проведения (локации) 
        public string ShowEventByLocation(string location)
        {
            bool flag = false;  // Флаг для определения, существует ли вообще такая локация в списке
            foreach (string needLocation in locations)  // Проверяем, есть ли такая локация в принципе
            {
                if (needLocation == location)
                {
                    flag = true;    // Устанавливаем флаг в true
                }
            }
            if (flag == true)   // Если локация найдена (флаг true)
            {
                for (int i = 0; i<events.Count; i++)    // Ищем конкретное событие по локации
                {
                    if (location == locations[i])   // Если локация события совпадает с искомой
                    {
                        return events[i];
                    }
                }
            }
            return "None";          // Если локация не найдена или событие для нее отсутствует, возвращаем "None"
        }

        // Метод для вывода указанного количества событий (count) 
        public string CountEvents(int count)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i< count;i++)
            {
                result.AppendLine($"{i+1}. {events[i]} ({types[i]})\nМесто: {locations[i]}\nДата: {dates[i]}\nОрганизатор: {organizators[i]}");
            }
            return result.ToString();
        }

        // Метод для поиска и отображения всех событий определенного типа
        public string ShowAllEventsByType(string type)
        {
            bool flag = false;  // Флаг для проверки существования указанного типа
            foreach (string Type in types)
            {
                if (Type == type)
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                for (int i = 0; i < events.Count; i++)
                {
                    if (type == types[i])
                    {
                        return events[i];
                    }
                }
            }
            return "None";
        }

        // Метод для поиска и отображения событий по организатор
        public string ShowAllEventsByOrg(string org)
        {
            bool flag = false;
            foreach (string Org in organizators)
            {
                if (Org == org)
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                for (int i = 0; i < events.Count; i++)
                {
                    if (org == organizators[i])
                    {
                        return events[i];
                    }
                }
            }
            return "None";
        }

        // Метод для редактирования различных атрибутов события
        public void EditEvents(int id, string editType,string edit)
        {
            if (editType == "event")
            {
                Console.WriteLine("new event - ");
                events[id] = edit;  // Обновляем название события

            }
            else if (editType == "location")
            {
                Console.WriteLine("new location - ");
                locations[id] = edit;   // Обновляем место проведения
            }
            else if (editType == "date")
            {
                Console.WriteLine("new date - ");
                dates[id] = DateTime.Parse(edit);   // Обновляем дату (с преобразованием строки)
            }
            else if (editType == "types")
            {
                Console.WriteLine("new type - ");
                types[id] = edit;   // Обновляем тип события
            }
            else if (editType == "organizators")
            {
                Console.WriteLine("new organizator - ");
                organizators[id] = edit;   // Обновляем организатора
            }

        }
    }
}