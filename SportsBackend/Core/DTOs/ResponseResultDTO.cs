﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ResponseResultDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public static ResponseResultDTO Succeeded(string msg = "The Process Done Successfully")
        {
            return new ResponseResultDTO
            {
                Success = true,
                Message = msg
            };
        }
        public static ResponseResultDTO Failed(string msg = "There are a problem occurred")
        {
            return new ResponseResultDTO
            {
                Success = false,
                Message = msg
            };
        }
        
    }
}
