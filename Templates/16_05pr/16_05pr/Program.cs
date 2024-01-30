using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _16_05pr
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string Students = "students.xml";
            var xmlStudents = XElement.Load(Students);

            string Teachers = "teachers.xml";
            var xmlTeachers = XElement.Load(Teachers);

            string Results = "results.xml";
            var xmlResults = XElement.Load(Results);
            
            //Console.WriteLine(xmlStudents);
            
            var students = xmlStudents.Elements("student")
                                       .Select(s => new
                                       {
                                           StudentTicketNumber = (int)s.Element("studentTicketNumber"),
                                           LastName = (string)s.Element("lastName"),
                                           FirstName = (string)s.Element("firstName"),
                                           GroupName = (string)s.Element("groupName")
                                       }).ToList();

            var teachers = xmlTeachers.Elements("teacher")
                                      .Select(t => new
                                      {
                                          IdentificationNumber = (int)t.Element("identificationNumber"),
                                          LastName = (string)t.Element("lastName"),
                                          FirstName = (string)t.Element("firstName")
                                      }).ToList();

            var results = xmlResults.Elements("disciplineResult")

                                    .SelectMany(dr => dr.Elements("studentScores")
                                    .Elements("score")
                                    .Select(s => new
                                    {
                                        DisciplineName = (string)dr.Element("disciplineName"),
                                        TeacherId = (int)dr.Element("teacherId"),
                                        StudentTicketNumber = (int)s.Element("studentTicketNumber"),
                                        ScoreValue = (int)s.Element("scoreValue")
                                    })).ToList();

            //Task A
            //xml-файл, де результати систематизованi за схемою <назва дисциплiни, прiзвище та iнiцiаливикладача, назва групи,
            //перелiк результатiв у виглядi пар <прiзвище та iнiцiали студента, кiлькiсть балiв>;
            //вмiст впорядкувати у лексико-графiчному порядку за назвою дисциплiни, назвою групи i прiзвищем студента;

            var allxmlA = from result in results
                          join student in students on result.StudentTicketNumber equals student.StudentTicketNumber
                          join teacher in teachers on result.TeacherId equals teacher.IdentificationNumber
                          orderby result.DisciplineName, student.GroupName, student.LastName
                          group result by new { result.DisciplineName, Teacher = teacher, student.GroupName } into g
                          select new
                          {
                              DisciplineName = g.Key.DisciplineName,
                              Teacher = g.Key.Teacher,
                              GroupName = g.Key.GroupName,
                              Results = g.Select(r => new { Student = students.FirstOrDefault(s => s.StudentTicketNumber == r.StudentTicketNumber), r.ScoreValue })
                          };

            var taskA = new XElement("results",
                from item in allxmlA
                select new XElement("disciplineResult",
                    new XElement("disciplineName", item.DisciplineName),
                    new XElement("teacher",
                        new XElement("lastName", item.Teacher.LastName),
                        new XElement("firstName", item.Teacher.FirstName)
                    ),
                    new XElement("groupName", item.GroupName),
                    new XElement("studentScores",
                        from result in item.Results
                        select new XElement("score",
                            new XElement("studentName", $"{result.Student.LastName} {result.Student.FirstName}"),
                            new XElement("scoreValue", result.ScoreValue)
                        )
                    )
                )
            );
            var taskAXML = new XDocument();
            taskAXML.Add(taskA);
            taskAXML.Save(@"D:\Programming\C#\Univer\16_05pr\16_05pr\taskA.xml");


            //Task B
            //xml-файл, де результати систематизованi за схемою <назва групи, перелiк результатiв у виглядi
            //<прiзвище та iнiцiали студента> та пари <назва дисциплiни, кiлькiсть балiв>
            //вмiст впорядкувати у лексико-графiчному порядку за назвою групи i прiзвищем студента

            var allxmlB = from result in results
                          join student in students on result.StudentTicketNumber equals student.StudentTicketNumber
                          join teacher in teachers on result.TeacherId equals teacher.IdentificationNumber
                          orderby student.GroupName, student.LastName
                          group new { result.DisciplineName, result.ScoreValue, student.LastName, student.FirstName } by new { student.GroupName } into g
                          select new
                          {
                              GroupName = g.Key.GroupName,
                              Results = g.Select(r => new { DisciplineName = r.DisciplineName, ScoreValue = r.ScoreValue, Student = $"{r.LastName} {r.FirstName}" })
                          };

            var taskB = new XElement("results",
                from item in allxmlB
                select new XElement("groupResult",
                       new XElement("groupName", item.GroupName),
                       new XElement("studentResults",
            from result in item.Results
            select new XElement("result",
                new XElement("studentName", result.Student),
                new XElement("disciplineName", result.DisciplineName),
                new XElement("scoreValue", result.ScoreValue)
                        )
                    )
                )
            );
            var taskBXML = new XDocument();
            taskBXML.Add(taskB);
            taskBXML.Save(@"D:\Programming\C#\Univer\16_05pr\16_05pr\taskB.xml");


            //Task C
            //xml-файл, описаний у попередньому завданнi, але без
            //врахування студентiв з незадовiльними балами(меншими 51)

            var allxmlC = from result in results
                          join student in students on result.StudentTicketNumber equals student.StudentTicketNumber
                          join teacher in teachers on result.TeacherId equals teacher.IdentificationNumber
                          orderby student.GroupName, student.LastName
                          group new { result.DisciplineName, result.ScoreValue, result.StudentTicketNumber } by new { student.GroupName, Student = student.LastName } into g
                          select new
                          {
                              GroupName = g.Key.GroupName,
                              Results = g.Where(r => r.ScoreValue >= 51) 
                                         .Select(r => new { DisciplineName = r.DisciplineName, ScoreValue = r.ScoreValue, StudentTicketNumber = r.StudentTicketNumber })
                          };

            var taskC = new XElement("root",
                new XElement("results",
                    from item in allxmlC
                    select new XElement("groupResult",
                        new XElement("groupName", item.GroupName),
                        new XElement("studentResults",
                            from result in item.Results
                            select new XElement("result",
                                new XElement("studentTicketNumber", result.StudentTicketNumber),
                                new XElement("disciplineName", result.DisciplineName),
                                new XElement("scoreValue", result.ScoreValue)
                            )
                        )
                    )
                )
            );
            var taskCXML = new XDocument();
            taskCXML.Add(taskB);
            taskCXML.Save(@"D:\Programming\C#\Univer\16_05pr\16_05pr\taskC.xml");

            var studentRanking = from result in results
                                 join student in students on result.StudentTicketNumber equals student.StudentTicketNumber
                                 where result.ScoreValue >= 51
                                 group result by student into g
                                 let totalScore = g.Sum(r => r.ScoreValue)
                                 orderby totalScore descending
                                 select new
                                 {
                                     Student = g.Key,
                                     TotalScore = totalScore
                                 };

            //taskD
            //xml-файл, в якому подано рейтинг студентiв за сумарною кiлькiстю балiв з усiх дисциплiн без
            //врахування студентiв з незадовiльними балами.

            var taskDxml = new XElement("ranking",
                from student in students
                 let totalScore = results.Where(result => result.StudentTicketNumber == student.StudentTicketNumber && result.ScoreValue >= 51)
                             .Sum(result => result.ScoreValue)
                              where totalScore > 51
                              orderby totalScore descending
                select new XElement("student",
                       new XElement("studentTicketNumber", student.StudentTicketNumber),
                       new XElement("lastName", student.LastName),
                       new XElement("firstName", student.FirstName),
                       new XElement("totalScore", totalScore)
                            )
                        );

            var taskDXML = new XDocument(taskDxml);
            taskDXML.Save(@"D:\Programming\C#\Univer\16_05pr\16_05pr\taskD.xml");

        }
    }
}
