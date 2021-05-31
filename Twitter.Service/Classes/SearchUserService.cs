using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.DTOs;
using Twitter.Data.Models;
using Twitter.Repository.Interfaces;


namespace Twitter.Service.Classes
{
    public class SearchUserService : BaseService
    {
        private IRepository<ApplicationUser> _repository;
        private ISearch<ApplicationUser> _searchRepository;
        public SearchUserService(
            IRepository<ApplicationUser> repository,
            ISearch<ApplicationUser> searchRepository,
            IMapper mapper) : base(mapper)
        {
            _repository = repository;
            _searchRepository = searchRepository;
        }

        public int CountEntity()
        {
            return _repository.CountEntity();
        }
        public int CountEntityByKeyword(string keyword)
        {
            return _searchRepository.CountEntityByKeyword(keyword);
        }
        public IEnumerable<UserDetails> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<UserDetails>>(_repository.GetPageRecords(pageSize, pageNumber));
        }

        public IEnumerable<UserDetails> GetPageByKeywords(SearchModel searchModel)
        {
            return Mapper.Map<List<UserDetails>>(_searchRepository.GetPageByKeywords(searchModel));
        }
    }
}
