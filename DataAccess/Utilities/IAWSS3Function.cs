using DataAccess.Entities;
using System;

namespace DataAccess.Utilities
{
    public interface IAWSS3Function 
    {
        EmailIdea LoadEmlS3(String key);
    }
}
