using AutoMapper;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.UnitsOfWork.Contracts;
using System.Collections.Generic;

namespace Lingva.BusinessLayer.Services
{
    public class GroupManagementService : IGroupManagementService
    {
        private readonly IUnitOfWorkGroupManagement _unitOfWork;
        private readonly IMapper _mapper;
           
        public GroupManagementService(IUnitOfWorkGroupManagement unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<Group> GetGroupsList()
        {
            return _unitOfWork.Groups.GetList();
        }

        public Group GetGroup(int id)
        {
            Group group = _unitOfWork.Groups.Get(id);
            return group;
        }

        public void AddGroup(Group group)
        {
            //if (!ExistGroup(group))//??
            //{
                _unitOfWork.Groups.Create(group);
                _unitOfWork.Save();
            //}
        }

        public void UpdateGroup(int id, Group group)
        {
            Group currentGroup = _unitOfWork.Groups.Get(id);
            _mapper.Map<Group, Group>(group, currentGroup);
            _unitOfWork.Groups.Update(currentGroup);
            _unitOfWork.Save();
        }

        public void DeleteGroup(int id)
        {
            Group group = _unitOfWork.Groups.Get(id);

            if (group == null)
            {
                return;
            }

            _unitOfWork.Groups.Delete(group);
            _unitOfWork.Save();
        }

        //private bool ExistGroup(Group myEvent)//??
        //{
            //return _unitOfWork.DictionaryRecords.Get(c => c.UserId == myEvent.UserId
            //                            && c.WordName == myEvent.WordName
            //                            && c.Translation == myEvent.Translation
            //                            && c.LanguageName == myEvent.LanguageName) != null;
        //}
    }
}

