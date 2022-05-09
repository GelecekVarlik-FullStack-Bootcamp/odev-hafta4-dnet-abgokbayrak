using BMS.Bll.Mapper;
using BMS.Dal.Abstract;
using BMS.Entity.Base;
using BMS.Entity.IBase;
using BMS.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BMS.Bll
{
    public class GenericManager<T, TDto> : IGenericService<T, TDto> where T : EntityBase
                                                                    where TDto : DtoBase
    {
        private readonly IServiceProvider service;
        private readonly IGenericRepository<T> repository;
        private readonly IUnitOfWork unitOfWork;
        
        public GenericManager(IServiceProvider service)
        {
            this.service = service;
            this.unitOfWork = service.GetService<IUnitOfWork>();
            this.repository = unitOfWork.GetRepository<T>();
        }

        public IResponse<TDto> Add(TDto model, bool saveChanges = false)
        {
            try
            {
                var mapped = ObjectMapper.Mapper.Map<TDto, T>(model);
                var result = repository.Add(mapped);
                if (saveChanges)
                    Save();
                return new SuccessResponse<TDto>("Kayıt başarılı bir şekilde eklenmiştir.", ObjectMapper.Mapper.Map<TDto>(result));
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi : {ex.Message}");
            }

        }

        public async Task<IResponse<TDto>> AddAsync(TDto model, bool saveChanges = false)
        {
            try
            {
                var mapped = ObjectMapper.Mapper.Map<TDto, T>(model);
                var result = await repository.AddAsync(mapped);
                if (saveChanges)
                    Save();
                return new SuccessResponse<TDto>("Kayıt başarılı bir şekilde eklenmiştir.", ObjectMapper.Mapper.Map<TDto>(result));
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public IResponse<bool> DeleteById(int id, bool saveChanges = false)
        {
            try
            {
                var result = repository.Delete(id);
                if (saveChanges)
                    Save();
                return new SuccessResponse<bool>("Kayıt başarılı bir şekilde silinmiştir.", result);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<bool>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public IResponse<bool> Delete(TDto model, bool saveChanges = false)
        {
            try
            {
                var mapped = ObjectMapper.Mapper.Map<TDto, T>(model);
                var result = repository.Delete(mapped);
                if (saveChanges)
                    Save();
                return new SuccessResponse<bool>("Kayıt başarılı bir şekilde silinmiştir.", result);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<bool>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public IResponse<TDto> Find(int id)
        {
            try
            {
                var entity = repository.Find(id);
                return new SuccessResponse<TDto>(String.Empty, ObjectMapper.Mapper.Map<TDto>(entity));
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public IResponse<List<TDto>> GetAll()
        {
            try
            {
                var entities = repository.GetAll();
                var response = entities.Select(x => ObjectMapper.Mapper.Map<TDto>(x)).ToList();
                return new SuccessResponse<List<TDto>>(String.Empty, response);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<List<TDto>>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public IResponse<List<TDto>> GetAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entities = repository.GetAll(expression);
                var response = entities.Select(x => ObjectMapper.Mapper.Map<TDto>(x)).ToList();
                return new SuccessResponse<List<TDto>>(string.Empty, response);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<List<TDto>>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public IResponse<TDto> SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entity = repository.SingleOrDefault(expression);
                var response =  ObjectMapper.Mapper.Map<TDto>(entity);
                return new SuccessResponse<TDto>(string.Empty, response);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public IResponse<IQueryable<TDto>> GetQueryable()
        {
            try
            {
                var q = repository.GetQueryable();
                var response = q.Select(x => ObjectMapper.Mapper.Map<TDto>(x));
                return new SuccessResponse<IQueryable<TDto>>(String.Empty, response);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IQueryable<TDto>>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        public void Save()
        {
            unitOfWork.SaveChanges();
        }

        public IResponse<TDto> Update(TDto model, bool saveChanges = false)
        {
            try
            {
                var mapped = ObjectMapper.Mapper.Map<TDto, T>(model);
                var entity = repository.Update(mapped);
                if (saveChanges)
                    Save();
                return new SuccessResponse<TDto>("Kayıt başarılı bir şekilde güncellenmiştir.", ObjectMapper.Mapper.Map<TDto>(entity));
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }
    }
}