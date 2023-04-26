using Core.Dtos;
using DataLayer;
using DataLayer.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Core.Services
{
    public class GradeService
    {
        private readonly UnitOfWork unitOfWork;

        public GradeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public GradeAddDto AddGrade(GradeAddDto payload)
        {
            if(payload == null) return null;

            var grade = new Grade()
            {
                Value = payload.Value,
                Course = payload.Course,
                DateCreated = DateTime.Now,
                StudentId = payload.StudentId,
            };

            unitOfWork.Grades.Insert(grade);
            unitOfWork.SaveChanges();

            return payload;
        }

        public ICollection<Grade> GetStudentGradesOrdered(int studentId)
        {
            return unitOfWork.Grades.GetAll().Where(x => x.StudentId == studentId).OrderBy(date => date.DateCreated).ToList();
        }

        public ICollection<Grade> GetAll()
        {
            return unitOfWork.Grades.GetAll().ToList();
        }

    }
}
