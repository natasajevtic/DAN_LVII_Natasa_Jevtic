﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Zadatak_1_WCF
{    
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Article> ViewArticles();
    }    
}
