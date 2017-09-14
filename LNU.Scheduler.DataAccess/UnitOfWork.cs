using System;
using LNU.Scheduler.Contracts;
using LNU.Scheduler.Models;

namespace LNU.Scheduler.DataAccess
{
    public class UnitOfWork : 
        IDisposable,
        IUnitOfWork<Group>,
        IUnitOfWork<Lecture>,
        IUnitOfWork<Room>,
        IUnitOfWork<Subject>,
        IUnitOfWork<Teacher>
    {
        private readonly SchedulerContext _context = new SchedulerContext();

        private Repository<Group> _groupRepository;
        private Repository<Lecture> _lectureRepository;
        private Repository<Room> _roomRepository;
        private Repository<Subject> _subjectRepository;
        private Repository<Teacher> _teacherRepository;


        IRepository<Lecture> IUnitOfWork<Lecture>.Repository
        {
            get
            {

                _lectureRepository = _lectureRepository ?? new Repository<Lecture>(_context);

                return _lectureRepository;
            }
        }

        IRepository<Room> IUnitOfWork<Room>.Repository
        {
            get
            {

                _roomRepository = _roomRepository ?? new Repository<Room>(_context);

                return _roomRepository;
            }
        }

        IRepository<Subject> IUnitOfWork<Subject>.Repository
        {
            get
            {
                _subjectRepository = _subjectRepository ?? new Repository<Subject>(_context);

                return _subjectRepository;
            }
        }

        IRepository<Teacher> IUnitOfWork<Teacher>.Repository
        {
            get
            {
                _teacherRepository = _teacherRepository ?? new Repository<Teacher>(_context);

                return _teacherRepository;
            }
        }

        IRepository<Group> IUnitOfWork<Group>.Repository
        {
            get
            {
                _groupRepository = _groupRepository ?? new Repository<Group>(_context);

                return _groupRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
