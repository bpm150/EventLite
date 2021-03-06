﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public static class ApiPaths
    {
        public static class Catalog
        {

            public static string GetEventsApiPath_StringConcatVer(
                string baseUri,
                int? catalogFormatId,
                int? catalogTopicId,
                DateTime? earliestStart,
                DateTime? latestStart,
                bool? hasFreeTicketType,
                int? pageIndex,
                int? pageSize)
            {    
                var apiPath = baseUri + "events?";


                // Setup [FromQuery] params
                #region

                string queryParams = String.Empty;

                if (catalogFormatId.HasValue)
                {
                    queryParams = "catalogFormatId=" + catalogFormatId;
                }

                if (catalogTopicId.HasValue)
                {
                    queryParams += "&catalogTopicId=" + catalogTopicId;
                }

                if (earliestStart.HasValue)
                {
                    queryParams += "&earliestStart=" + earliestStart;
                }

                if (latestStart.HasValue)
                {
                    queryParams += "&latestStart=" + latestStart;
                }

                if (hasFreeTicketType.HasValue)
                {
                    queryParams += "&hasFreeTicketType=" + hasFreeTicketType;
                }

                if (pageIndex.HasValue)
                {
                    queryParams += "&pageIndex=" + pageIndex;
                }

                if (pageSize.HasValue)
                {
                    queryParams += "&pageSize=" + pageSize;
                }

                // If there was no catalogFormatId
                if (queryParams.StartsWith('&'))
                {
                    queryParams = queryParams.Remove(0, 1);
                }

                #endregion


                apiPath += queryParams;
                

                // Assuming there is no valid way for there to be a '?'
                // in the baseUri
                // Assuming there is no valid way for there to be a '?'
                // in a DateTime
                               

                // If there were no [FromQuery] parameters at all
                if (apiPath.EndsWith('?'))
                {
                    apiPath = apiPath.Remove(apiPath.Length - 1);
                }

                return apiPath;
            }

            public static string GetEventsApiPath_StringBuilderVer(
                string baseUri,
                int? catalogFormatId,
                int? catalogTopicId,
                DateTime? earliestStart,
                DateTime? latestStart,
                bool? hasFreeTicketType,
                int? pageIndex,
                int? pageSize)
            {
                // Build the [FromQuery] params first, left to right
                #region

                var apiPathBuilder = new StringBuilder(200);

                if (catalogFormatId.HasValue)
                {
                    apiPathBuilder.Append($"&catalogFormatId={catalogFormatId}");
                    // Yes, this & will always get removed.
                    // Making the removal non-conditional is easier to understand later
                }

                if (catalogTopicId.HasValue)
                {
                    apiPathBuilder.Append($"&catalogTopicId={catalogTopicId}");
                }

                if (earliestStart.HasValue)
                {
                    apiPathBuilder.Append($"&earliestStart={earliestStart}");
                }

                if (latestStart.HasValue)
                {
                    apiPathBuilder.Append($"&latestStart={latestStart}");
                }

                if (hasFreeTicketType.HasValue)
                {
                    apiPathBuilder.Append($"&hasFreeTicketType={hasFreeTicketType}");
                }

                if (pageIndex.HasValue)
                {
                    apiPathBuilder.Append($"&pageIndex={pageIndex}");
                }

                if (pageSize.HasValue)
                {
                    apiPathBuilder.Append($"&pageSize={pageSize}");
                }
                #endregion


                // Finish building the apipath right to left 
                if (apiPathBuilder.Length > 0)
                {
                    // Change the leading '&' to be the '?' for the [FromQuery] params
                    apiPathBuilder.Remove(0, 1).Insert(0, '?');
                }
                apiPathBuilder.Insert(0, $"{baseUri}events");

                return apiPathBuilder.ToString();
            }


            // GetFormatsApiPath

            // GetTopicsApiPath
            public static string GetAllTopics(string baseUri, int? catalogTopicId)
            {
                return $"{baseUri}randomevents?catalogtopics={catalogTopicId}";
            }

            // GetSingleEventApiPath
            public static string GetSingleEvent(string baseUri, int? id)
            {
                return $"{baseUri}singleevent/{id}";
            }



            // GetRandomEventsApiPath
            public static string GetRandomEventsApiPath(string baseUri)
            {
                return $"{baseUri}randomevents";
            }



            // ...more?

        }





        //        Next: 

        //Components of webmvc project

        //need to touch all these places

        //views
        //viewmodels
        //viewcomponents

        //controller
        //services
        //apipaths
        //customhttpclient



        //start with apipaths(start from the back)


            // Then to CustomHttpClient
        public static class Basket
        {
            public static string GetBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(string baseUri)
            {
                return baseUri;
            }

            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }

        public static class Order
        {
            public static string GetOrder(string baseUri, string orderId)
            {
                return $"{baseUri}/{orderId}";
            }

            //public static string GetOrdersByUser(string baseUri, string userName)
            //{
            //    return $"{baseUri}/userOrders?userName={userName}";
            //}
            public static string GetOrders(string baseUri)
            {
                return baseUri;
            }
            public static string AddNewOrder(string baseUri)
            {
                return $"{baseUri}/new"; // Firing create order method
                // No changes to httpclient (so don't need to go to CustomHttpClient)
                // Move on to models...
            }
        }
    }
}
