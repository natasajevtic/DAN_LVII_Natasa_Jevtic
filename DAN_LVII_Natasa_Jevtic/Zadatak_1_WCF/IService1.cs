using System.Collections.Generic;
using System.ServiceModel;

namespace Zadatak_1_WCF
{    
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Article> ViewArticles();
        [OperationContract]
        bool AddArticle(Article article);
        [OperationContract]
        bool UpdateArticle(Article article);
        [OperationContract]
        bool CreateBill(string bill);
    }    
}
