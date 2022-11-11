using Final_project_webapi.Data;
using Final_project_webapi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Final_project_webapi.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly DataContext context;

        public UserService(DataContext context)
        {
            this.context = context;
        }


        public async Task<ServiceResponse<User>> AddUser(User user)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            try
            {
                context.Users.Add(users);
                context.SaveChanges();

                serviceResponse.Data = users;
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> DeleteUser(int id)
        {
            ServiceResponse<List<User>> serviceResponse = new ServiceResponse<List<User>>();

            try
            {
                var user = context.Users.First(user => user.UserId == id);
                context.Users.Remove(user);
                context.SaveChanges();
                serviceResponse.Data = context.Users.ToList();

            }
            catch (Exception Ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = Ex.Message;
            }

            return serviceResponse;

        }


        public async Task<ServiceResponse<List<User>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            try
            {

                serviceResponse.Data = context.Users.ToList();

            }
            catch (Exception Ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = Ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<User>> GetUserById(int id)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();

            try
            {
                var user = context.Users.First(user => user.UserId == id);
                serviceResponse.Data = user;
            }
            catch (Exception Ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = Ex.Message;
            }

            return serviceResponse;

        }



        public async Task<ServiceResponse<User>> UpdateUser(User users)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            try
            {
                User user = context.Users.First(user => user.UserId == users.UserId);

                user.UserType = users.UserType;
                user.Id = users.Id;
                user.Name = users.Name;
                user.Lastname = users.Lastname;
                user.Email = users.Email;

                serviceResponse.Data = users;
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Error = ex.Message;
            }


            return serviceResponse;
        }
    }
}