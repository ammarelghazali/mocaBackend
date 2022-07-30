﻿namespace MOCA.Core.DTOs.Events.EventReccuranceDtos.Request
{
    public class get_AllEventReccurance_Query
    {
        public get_AllEventReccurance_Query(int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            this.pageSize = pageSize <= 0 ? 10 : pageSize;
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
