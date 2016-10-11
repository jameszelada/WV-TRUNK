using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using WV.WebApplication.Reports;
using Repository;
using WV.WebApplication.Utils;
using System.Web.UI.DataVisualization.Charting;
using DataLayer;
using System.Xml.Linq;

namespace WV.WebApplication.Pages
{
    public partial class About : PageBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //Dictionary<string,Object> param= new Dictionary<string,Object>();
            //param.Add("userName", "Jaime");
            //param.Add("roleName", "ADMIN");
            
            //string query = "dbo.spGetUserInfo;";
            //DataSet myDataset = GetDataSet(query,CommandType.StoredProcedure,param);
            
            //var X=0;


           

            //if (ValidateSession())
            //{
            //    AddUserTag();
            //    ValidateOptions();
            //    if (!hasPermissions(pagename.InnerHtml))
            //    {
            //        Context.Response.Redirect("Unauthorized");
            //    }
            //}
           
        }

        


        protected void Button1_Click(object sender, EventArgs e)
        {
    //       string base64 = @"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LyIgeGFwUmlnaHRzOk1hcmtlZD0iVHJ1ZSIgeGFwUmlnaHRzOldlYlN0YXRlbWVudD0iaHR0cDovL3Byby5jb3JiaXMuY29tL3NlYXJjaC9zZWFyY2hyZXN1bHRzLmFzcD90eHQ9NDItMTU1NjQ5NzgmYW1wO29wZW5JbWFnZT00Mi0xNTU2NDk3OCI+DQoJCQk8ZGM6cmlnaHRzPg0KCQkJCTxyZGY6QWx0Pg0KCQkJCQk8cmRmOmxpIHhtbDpsYW5nPSJ4LWRlZmF1bHQiPsKpIENvcmJpcy4gIEFsbCBSaWdodHMgUmVzZXJ2ZWQuPC9yZGY6bGk+DQoJCQkJPC9yZGY6QWx0Pg0KCQkJPC9kYzpyaWdodHM+DQoJCQk8ZGM6Y3JlYXRvcj48cmRmOlNlcSB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPjxyZGY6bGk+Q29yYmlzPC9yZGY6bGk+PC9yZGY6U2VxPg0KCQkJPC9kYzpjcmVhdG9yPjwvcmRmOkRlc2NyaXB0aW9uPg0KCQk8cmRmOkRlc2NyaXB0aW9uIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyI+PHhtcDpSYXRpbmc+NDwveG1wOlJhdGluZz48eG1wOkNyZWF0ZURhdGU+MjAwOC0wMi0xMVQxOTozMjo0My4xNzNaPC94bXA6Q3JlYXRlRGF0ZT48L3JkZjpEZXNjcmlwdGlvbj48cmRmOkRlc2NyaXB0aW9uIHhtbG5zOk1pY3Jvc29mdFBob3RvPSJodHRwOi8vbnMubWljcm9zb2Z0LmNvbS9waG90by8xLjAvIj48TWljcm9zb2Z0UGhvdG86UmF0aW5nPjYzPC9NaWNyb3NvZnRQaG90bzpSYXRpbmc+PC9yZGY6RGVzY3JpcHRpb24+PC9yZGY6UkRGPg0KPC94OnhtcG1ldGE+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwACAQECAQECAgICAgICAgMFAwMDAwMGBAQDBQcGBwcHBgcHCAkLCQgICggHBwoNCgoLDAwMDAcJDg8NDA4LDAwM/9sAQwECAgIDAwMGAwMGDAgHCAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwM/8AAEQgAXQB7AwEiAAIRAQMRAf/EAB8AAAEFAQEBAQEBAAAAAAAAAAABAgMEBQYHCAkKC//EALUQAAIBAwMCBAMFBQQEAAABfQECAwAEEQUSITFBBhNRYQcicRQygZGhCCNCscEVUtHwJDNicoIJChYXGBkaJSYnKCkqNDU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6g4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2drh4uPk5ebn6Onq8fLz9PX29/j5+v/EAB8BAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKC//EALURAAIBAgQEAwQHBQQEAAECdwABAgMRBAUhMQYSQVEHYXETIjKBCBRCkaGxwQkjM1LwFWJy0QoWJDThJfEXGBkaJicoKSo1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoKDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uLj5OXm5+jp6vLz9PX29/j5+v/aAAwDAQACEQMRAD8A+f7XSQcHFc94h0+OTXLkzylYUgKxqR8hbgEE+v0Hau5t4dpH69s0fCD4Ga5+0L8WbDwpoUcc+raxdeXHI8TyJaqz43OEVjgck4GeOOTXwFO7kkj62grXl5HkPw28B6n42+LPh/QNNs7/AFTUtQvFitLaK3fzbqT+FY16k+g9q/R74Xf8Ek/EF1eJN4+8R+HfAcMPlXFxZT3CXOoeS2Gz5UZbYx+6BLt+Ygc8ivSPET/Cn/gihoC6f4dtm1P4l61YrD4j8TxMJXidQjFLZLiYxW27ccBuTkEg4Ar45+O3/BcLxHrHiYmJoNODXP2m0g1F4tUmkVEdDjy/LYk42tLiKNCGCwq25x7f1SDa59Wuh51Sq5O60X4n218RP2f/ANmHwFZDT7vwn4z3QwE2uq6Zq6Tz6oyqd7mNWdUcMrDyygOCSFKgbT4ZeF/2N/EfhOW0F74j0O8W7jNzcazeJBJYPIvlgM+3yxHHIwYnGN0IJypKt8QfA79rPxL+0mlxfWEmqw3EjMspgsrW+WQemyEbZY1VRhZW3ZAH8IrV+M/w107x34M1pbqy04eJfEUMOlWunTSTQXes3shQIzQHBhRHRQWXc6qFQZDIE6lQp78i+45nvZNnsn7T37Hln8OtKsPE/gy5bWPBepxoIZzcJLcJKU3lWRc4Xblg2SMYXOQN3I/sx/Aef4vfE20swm63idXlOOvPArpf2PP26bH9m6/1j4Q3urab4otbKC10+98PTwtc2E9qZHgja0dzK0iMVm4kWMOGRwh4d/u79lv9jGw8LyzeIvDbS2FjqmZYrHU1ImsiQPkEiBlkTkFXBzgqCAxIHk4vASUr0Vv0O7B1KbknWdkjiPjhomn/AAJ+HIS08uOW3hAGMdcV8TRWKfFfxBrN0jTRRXtt5Nzvbctm5b5Jc/3d3B//AF191ftl/sffEn4rtHa6BeeF52ADTWsmq+XOozgnDIB0Oevrz6/JNx+yf8WfhN4lulj8DeKLlrZWjme00uW9tbiPuRJErI698g/kQQPMxeDqxiuVNS/PyZ143Exru0WrHhHi3TJG1p4pQrS2qi3crgjcvB57/WsW50X5SdpwPbNel+MPh3qWl+J7pbqwutPLL54huoHgkwRkYVgD7V2/wV/ZmtvFcK6z4o1rTPDPhwuIftN/IERnI3kY7kR7n5wMLyecDKg5KEacFd6Kx5s6XuqbPmO900FyQvSvQ/BOj33hD4YTavbLATNdeW2V/eAgdvbivsf4eeEP2cfGmn6Pd6ToXiHXJvFEKR6Zb3Oo21jBdIzEs8QLF3eNEDEAHCyxsNyvuFjxJ8EvhpNb2tlL8N/FVv4Ud1EGvw6+3n+YzKF3RyRrE8ZaSNTIgZVXLFgMyD0a2UYitFQnbl66/hsc0anLK8dz89PEwudfuZLm7lknlfklznFcpPoGZm4PX1r7T8Wf8E97/wAdeFodT+H8c9xexfubrRNRu4jdy4iEv2iGRQsMitGyZVGO1twyQRj5SvNJaG5ZXyrDsQQaKlJ0bRkrAqTk7rUo2qh1xjJPYdSfSvtT/gnX8PfCvwwEmvyagRr97E3lGe3cJaJhg5BDHcACw38KT1wAc/F+jo88iqoXryWxjGec5IGPr1r6X+F/7VU3w30kz6fqM2j2sSCGc+Zg38uVRUXo7FuMJGFHLFiFUMc8O7VdT28P71CUvM5r9tP4T6N8TvH2oa1qn2jxT4giG21N+rx2enDc7jZBCzO4A3FUYhSyEEOA1fmp+0l4G0fwb44n0VLgalr9+VuNQkaVYorfnAjZd3RcAAEsMjgDaK/Rf9oT4ja9badrFxBA3hS0uyBNNfnbcuOCFAVn7KoEca4JwWYt1/O7V/Covfi0sLWlz9l1Of8AcG5hENxqDE5PlQ7ZJSTzz25PY19DRR5dWXvnu3/BOrw4/gjXLqTW21u2ktosWt1b5jtQ7DCJK7J+7O7kMrnp17HK/bL/AG1Jr3x4lvqviC51/TLHzrOLUJopSdEjkhVkVrraJ1aUxlBwGYJ8pQRsT6lquiaf4X+B5sPDuiaxpfiCS1NrLf6sblorGMpl2RSXK56fIFPIGCCFP59fBH4A+I/2xP2w38AaXK+p6Yl/9tuYImaOK+ijeOMAFAdrSgRrvb7v3mICsa7qcE1ZnnVKj5m0fV//AAS+0LWf2g/i/ofiea11KbTrCWCSGN7iKNNUdwITBGGMYWNJpLaNTkiNBzzJIZP2E/4X/eTi5h8vVtAubRLdLnS5oXRSjE+XBbujABEWCRCg2lXBRSJAS3P/ALIf/BP238MfC6xuIBLcaReST3FlPpdqdKhs7JkWFVRvLXcsmA7ZSXcgB2MNpr2bxV4M0nwh46upf7Pih0qKG1tZ7QWvlQW8zTKxaSOIeUzKwZG4xKqyD5XiUSc1aPPK62O/Dv2cEnufO3jn9pTx34fv5LJfD9/cS6Knnzahp9naQLIZY+ZoY5TK0ZaRpgQyA+XHnJ2ymXsfhr+1xq/gXRBdXhvvtU6JqF7O0txCLFppnWRdzPIixpKGGInb5YSrKuDj3Dx54I01Ll9LWPTbk6lZyGSeUBJXt0R4MylYnTILRhGYEA+YQAAceB/FL4OQXOl6jNBeQOItQAInimitrKMt9lkhiRH3OscaANh0yty5C/vecZUbM6Y1m1Znr7/8FMNKXwVZWep3Wl6pNqR8yZ9WEax2se5gBOMGNGdFOMKdzFdsfzMkX52f8Fb/ANvG88aaz4F02G2sY11m7ijsLeKdIrW0ElxBiVo4seYAZ3ibeqmRQ4XIkVlg/a/0lPgol5aWbOxiRGvbeJ0ls4Q0QE8bESBvN27yweLdKvAYFQo/MD48fFLXfj2fD2pX2pLY6x4Xla2FnDEVmt8iNirJNIULMwQx+XwqoewiWtaMHKSbOTETjCPurVn6tfD64+IXxF+Hdre+C08O+E4fEkMUl7ForXM+ryiMAJbyXkzIy26MolC2atAzSMwlbaCvzj+1B8dviX8G/irpn9sX8viexsVFqZYdRliaC9RAkixyOjJK7RpuUyyMfmX7oUk+b+Bv2+fFWifBfXdTYvqWty6TbCzfSxLZQybWIJk2yKYH5LymHdG8szqyoEVa8O8Z/tU6/wDF21ggn8MQaHfj99JLcPI12wI4+cKjjJx95ue9azp2M6NRzdkfp5+zv/wUy1fSvjlM80Phy40mQrBNsWK0k1C0mWGaNZY8fI5hmjdFZ2UPEOY42ZV+tr3VPBvjS5OpXPw00x57kDc82iQXMsu0BQ7yCEh2YKGJz1Jr8SfgB8QbGy0TTPDF+dZkXULpY7gCCS5S1tVRXRcBtyR/68nIJ5TaN2cfr/YeLviJDp9unhGzh1Hw+kSLbXUFpZXSTkKBI/mNIpJMm/PygA5A4AqeVTWpc7wlsfFnjz4b+Ifhhqlnpuq2D2N5eoJkTzVYmPcQT8pPGRXof7O3wd0zxv4zsvFPjTWLHQPC+kSssHlr59/eXUZU7lTOPKDumVLLnAHNeB/ALUdb1/4Zx65rmo3uqapqEK3FsboBmtVlIMVuQegUHg9eprzn4O/tSfDD4B/tNeNJvinJ4g1/WNJuI7Hw/okTebpiQeT5k0sqf89XlPcg9Px+cwdFTqyS6fpZH0GISw2Hil5b93qfpd8fvhPo5+HP9u6Dq2j+LYLuOQWGoPDgWhIYKfKj+XOAyA5ILFif4cfHXgPw/pXwh+O1vHObbWvFG7OraneYzpxbjybeIgjeCcMWOQuSckqo+3/+CTfjHwD+3voPiyTw4ZfDetalCWn0m4uAIbQqi+QbVDkIVZcOikjGGGetV9a/4J96RpPxGjh1hTpet6ErmPT5uZFkYmSScyfdIYHcHLE4Y46c+wqypSueJKh7ZOzPAf2h9ZsoNAuxAsFq93C6pcXMjkgN8pcjO7gA4GBk5xnFdJ/wRK/YAsLay8SfEIXlnb3/AIynfT5tQuLaOJ7fT7fe80qLJlAzSqmWb5h5GQCFbZ6HpvwA8KfFfxrL4beGaa7BeKOWEK0bP/ExOCSM8YHYDn16r9rX9pPRf+CYHwc8NRTXGh2vhm9ll8NW1vrdvePZXTQoJL6dPskgdJHfy1WTaVCvKSHKspdPGKrJxiwjgJ07Tl8vkfUHx0+MY8Lf2fo9nCj2MNm1tYQrG14Rv/cWpkKyB90oJbawI3xsd5I+Xzy4+KkvhV9NXWILm4nvoFksoJSs32EpsAWRnJBTaxUt5ezyy4B2r5Z5P9nzRfD7+FNH8R3cc+heFL20j8T2zareb59Ot5oo55C+7ABRpJQSMZKLz8yiuT+In/BV/wCDN3M8VppPjqTwtb3Edm/jy38IsdBkVpGJ8uZm3Ha6L+9EexsDbuXAXoXW24S93SWh7BrP7T174lvrSPTHtI4dQFuNQEQ2MrJE7SPLIWGH3Yh2PgBXywYsIxk3Gq2nie6l1U2cjX6Brm3mmlLQpPs+YyYcllRfNK+WpdRKv3FSfPmviW90/SdJkMF7aJY3sIvUvPKZYhGQMs7/ADFoyoykbJuCtISED14NZftU+IvDXxJ0/SPDlm9y8cnl2jDRxJcLNHA6rLI6FiWONpBcsQwIDGTdQ1dk+0UVqaXx30XTviBrFxrl9bxyW+nRDVZbaz1aeRLqKeEBbuV/mkaQIcCJ0XHknaUK+XX5Fft8x6PZeONO1Kwnu4IdWg+xapHtj8qLacxMqBd5X7O0KgO7SbopWbaHUV+x3i3xBFZeHfEWqavpd9Bqdpbpq9vPA0kN7aXLbGuZZHJyDGIEPlzHZhdrMsYLr8T/ALW/7N+nTfFrX0ns1V7jUzqUV1PaRQxXAZcLc7+giwI22nn94AXfgjSiuWd2c+IlzQ0OW/4Jrfswaf4w0S01HU9SudP0xzGkEtqxuJYgUjmXcuAzKBIdmMsU3ZQEgV6n+2z+yHrHgSa31yDTdBvNDllWNNds7bdNcOOizuiAg4HWaPacDkHrtfsn+HPBPhbwasWlBJrdHt5bmdJg8NoHMkTLNGGw21toJXDJ53ytgYr0L9vzxX4u0j4SaXDb6hpmozI8k8P2F3t572zG7EZiYArPbrIqsrofNVjkqNxbr9mpRbOKFVwmj5x/Z6tdIg8cDUb3w5qcmoaQAs93pttlryMlVkliKjypnTK7lQhioYgtggfdfhXwfq134bsrjw8/9qaHdwrc2N3bWMssU8Ug3qytGu0jDfX+8A2RXzf+y58K5Pifq/h/xH4TjfQTqkR84Qb1hkuIgdw8vkALtcKCSVGxcuC1fffh/wAU+EPDWh2lrB4km0KMxLP9iheURRNKPNYptKjazOWHyg4YZ5ya5qqpxdpOx005VJe8lc+O/jB4f0H9mW9vNOEEMVnZWu+FQAWzsYKcdB82DX4gfHfWrrVPjV4n1Oa6llu7/VLi5aUNyd8hYYPsDj8K/Sb/AIKQfHiTUfiDqMVtIwMoWOQbicckbc9uxOK+Mv2c/wBkq6/bL+KfiLTI71tM/srTZr03Yj3q05bbChH91mz05wDXgZIo0Ie1qPS2p9hxEnXk6VNa8zsfSn/BF79v3/hn7xTptzqbS3NzZ6hs3ROBPtbHzAY5OCwz7etf0ZL4v+E37ffwt0s6veQ6b4gkgW1s7xLhVvbd3UY5H3lYnOyQY56A81/Jn+zJ+zx4r8J/Fm8u9R0+4sRoTtbuCuTNJuxtQfxfdJz9K/Rr4FfE3xZ4O1LSr+xnnh/sx/PEBiw8zBsjJJGDnH4CvSxMYczcLNPp0Pl6E2rKd0116o/Wv4ZfsTar+yv4tkvtXuYNRsZZwkWq2y/IkQIxuU/cY9ME4z/Ea+U/+Cl3xj+G3x30pdO8d+D08TReE9UuLu00WaJZWgaICJcKzM/3I8Ntj2MMYA+63sv7LX7eGs/EDwbqWi67OXgnsUtjDdSiR5ZJAVU5wQGDheCeeo6kV4YPhtb+Nv2/obK4SLS9E1jUjf6xcRwsir5s2xEEgG5sR7ERCQFLGRVLnjnwFCnByUO56GNxVWUYOVtrJrrr27mv+0t8ddH8W/ADwTPqMQubLU7E3tzppjkgi1CKMgxRujY3wpcPdKVKhW8hAdwVRXz/AONf2qbb4leGl8PLpkNza30RiMWC6mNlKcfMNpCqOBwcEcY4+UP+C8H/AAUbsvF37cnijwz8Mprax8HeCxH4WtRYyjy5BZtIkrKycYadpWHPQg4znHyP8Mv22dc8G3UU3mM06k7ckhB9M9CSTk89q9unSS1seNWrSlJ3Z+iXgrxfrfwx8H2Xw/XV3vNKtdT8rTDNvedLe63Otvj7u1ZAykqB8pXJIXjlf2qfiV4l/Zq8IaVe+ErW68Wtq7x3mq2pnuUjnfYVCn7LIkrwo+P3cbhWBfeTgZ0fgV8FfEv7R/7PljqGmSx2/ittS/tqISLIbYkw7MbhhV2Dy2zng5x2FcF8J/Gr/A7XbnSvEupan9o0yZ5NV02/g820iEYKGXy4ULwjKsCFVsBQCcE4zcWndBTqpfFqfUn7PI1g/Bzwnr/iL7WI9f0mTT9ftNU1Iz32nrcvdLbqssqlyiwPChBkEiEYG07Xq/8AHLwLf+JvhzJFOljLq8Ms0byTrIytDII1KjYdvyMrxqAQdsYOF+Vak8LfEt/il4KNhrTST2d2LeGS3tUc2f2ZpC0Rxj7uVfDY3FYyX3KoY9HqmoR+MdDMnkgpqFs00ESs7ICWO4qxwQQ4ycE7/MZgRkCk0kiZT5medfsk/s6Wup6V4pt4s6fq98ZJLVZ4zFJLlVZvl4B2O0bHnkpnOSMek/tNfs5Xv7SE/gRdOeeyuNEaaO6mgP7iO5iCu6sh5BY+WCV6oz5H7sE+EXv7Wtz8G/23vB+nww/2ho2kFfD2rqrkSq12YIyzq27dGM44wwZFwcgBvqXX/FN1puj3F1KDHFfSpNLAJCWiu4T5LlX5BSWPJ9w3IBBA4q+IlRh3TOzDYeFZ7+8mP8FWWj/Bb4X3C6VBDb3Ut5FeKsD4QefFE7ShhyoLFgFHH3+hOB5prfiO+1m/886lNnyo4+SVJ2RqnQf7tJq/iC61NpFlld1mYu2e+XL4+m5icdKz2jUnkDP1r57EYudWV2z6CjhYU42SPzt/aUtJNZnkuZbks17I8wUHJB44z1I5NfQX/BKL4THwz8AdV8USIPtXivVHEbYwfs9vmNR9C/mGk/ZS/ZQ8MftJyT6p8QvEOqaJ4Z0wm1xZhFnklEQcZeT5VQ5K8c7scjOa+lfhV8N9K+Bvwu8NeEtL1KPV9P0exRIL5U2fbVfMnmFT91ju+Ydjkc4rarzex5H1Z1VJwlV5o9Dj/iV8ErW61FvE+m2wTVbYb7qNVwLlB/Hjs4HfuBXc/AzwzpV3qNlcypGsv+skjuAf3uemT681xH7Z37QEfwG+CdxcWMmNd1tvsliFXcU/vvj0ArC/Ya/aj0z4q+H4riS7MupxYt7u0YqDA49B1+YcgmtMPzqm2ePjIQ9spWP0ttv2bLLxP4FhuvA850+8aJYL37MuI5owQTJ/tFSOK8t/aW+Hd74C8eW2pvA0+l6zE6xGcFBDdJG6tu4xvBkVwx4G5sEdK9D/AGN/j5b+GdTeyvEHkxq9y+5lY+XxhRzxzjgelfSnxl+C+l/Hv4H67a388NrqN2I7uzuOQdPulQuM45IIYK2OoY9wKww2JnTrq+z3OnFUIVKGnTY/mm/4KH/sV63o/gi68e6h4QSL/hJrmS40e50UKkNrKJ3F1a3UTNvRlcNswNrI8ZDFg4r5J+HvwTuvEmpWUcul61LczSiKPTrOEm5n+XdvLkBUQn+I8Dv0Jr9wvjd4n1P4WaNq3w58f6HLb6Zd3EmoWN+iLNbxytjfhgQWilKq4YAusjSggBhXx/8AEfxx4Q+AN7NemCS51Fz/AKLZ2sG6a4btgkBVGcZJ6dhnAr6OOLailB3PEdOLd6i1R7P8FP2zPDvwL8JeE/gToUj/APCwoNKt5L6+mHlWk06xoCqOU+cufmG3CnJAySAfLPjla3WsfE1fEt7PYW+rpaStcXUNvJHHcoBJkzRAsJfvn53LMwztIArxT4cGfxX8QLz4h+ImRdYuIi8NsckwxhmwiPjgjjkc98DOK3rvxJqereJdUv7ZUumuLTyFjRiAE80J8wCkqcgn5R1B7DklXbdjndJbo+sfDHj2TXbC6srO4KwXXyo1vMvlyWqK8ackDASPcFPH7sDnmnfEL422Hw/+E+raxMzSR+H9ON3NBCzxG5kZk8q3VQCQWZk9fmc45GB4Bp/xK8O/Db4cPqzyrp9ncBoZHuMPNOZWUeUqp975sr/s8k4INfN/x5/anv8A443ttpVm/wBh0HTJlmgZbjyXubsDAuLhsEN959qEBVHUnmqg3PToZtKJ2sPxY1DUdf1e71rxDpl9q+tTtqdyjxiVxNcI7hiXLKIyrqvy4GY48FGILfU/7Cv7R2n6/wCG5PC/iC/S10fU5W+x3NxOqtplxCGWPeRwqSAkngLzgAs4NfEnw+tbaT7BHcOlqljZxHfeou0yxKI2hwxAXO5AzOpQbmxjgDuvDWvSfD++tgl+5SzjkiVpmkaZo3UARqFYq6RsxJEi8qGJXbxV16SnBxZeHquE1JH6Iajo0ljeTW9whSeBzHIp7MCQR+dA0pscA4qn8OvGT/EvwB4f8QSW8lvNrGnxTyRuCrbwNpbns+0OCM5Vwc5PHXJZxRoA33gOcCvjalPlm4dj66NWLSfc/Pj9oj4pat8C/iHYfB3wxp7NZi8zeXUkREmu3buQbhi33YmYoqofupEhPzOa+0fh58L9V0D4beH9Q8Wpc+D7E6VbzMlzEz3Sjy13HymIcc5+9gt2r9NfG/8AwT0+Ef7SUc3i/wAVeHru71G2l3K8V0scsmSVw0mwscBRjpwAPXNb9u//AIJp23xR/ZbXR/CXjO88FpYWojSSfTl1UtCQv7s7njPAGAd34dc+5VourGOlrbnl06yp1Jczu3t/kfiR8cfAerfGnx9dalYahY3ulx2vk2DMNqwW4bDOeTtcsDkduOTXyj8CtK8V/D39smTTvDlpNcSPHPJqdvE/ytBEpcyZ9V4+ufev0A1P9kPVPgn8F5PDo8ZRan58czz3J0cQGRhuYkKspC5KjgHiuT/4J5fszQ+BNO1jx5dasdX1XxEkFuolttgs4XhMzIp3nOSACcDIArioVqkHN293ZHq4mhRdCEX8T1Z6J+zV+0u+tX1rtnjaaaaNBkZ2KGBOSen61+nXwl+PVtrngbU59S1GG2gtj9qvp52PlQRbdxOep47DOeg7V+Q/xQ+G1t8LPihYzaXKYbXU5TcfZkTasLFgGAOehz6V7r4v+Let6H+zNq9xZTosrKzSLMDJFMiK3ysoKnPAwQRggcHpQ6anaUTzac5QbhLY57/goR/wVv8ABmv3t1Y6f8O9ZvNOiBs7a9vp4bRZcNt3KrMcZIwGYhc8ZzmvzJ/aC/aq1DxV4kY6RoS2NrGmAkl55p5ySeBjOAT+vY46H40P/wALO0CLxVuk0/U0s72e0eNsm0a3lmGeNofd5IPzA4JJ5OCPPvhDDL4ki0oX32N7fxFp+oXkcUVqqmxNqGlKh2LM+8ngk5THGelfSUcOuROS2Pn60/fbRyY+PHiVtUimFikioCqRzSsW+Xg9MHaDnOB2q5p3xY8a3WpLZ293Z2W+VVaCz2K0pZThSWJZgVbPy5wdvHNd3oPh3R9UttMvZdMi+0X1g9xIFkZEMUbKFj+XDbuVy27B2525ORDf6XD9r1BYwwmRrpppZAsxuHt0h2MdwJHNye+RtyCGZmOvsYLoZupJ9TmofCmu/EqxW48RXV9eW6bFtVuvMWCIBCxki6xqPlXJCngEHnONHVPC9xpXiGaKJLaO5Rn2+faEqE/hDsSY0J2kFeArAsepJ6nUfCUGi6te2dzHBeT6ZatFFN+9xLB5Y+SRXkfc2Sh3KVGYwNpDMDyFjdz6/fy2bNbrDpaMbdWgEiwoJGYBAfuOSFDODlgGHRiKpK2gr33Om+yRG1U6ZdO8cZEv2ppo3dSYyVgk8stGjoN7cqHDPIw7MbS6gmp6eqNdXcyRwsrvEiyTbRGHR5MKCVRgpZUBZSdxO4ZrktMjlvNE0jU5JnaCJYoltSS6ZbfGGO8sCBH8mCvKjGcVLpmk/wBqCwdZ5Y7mW8MM87EM0pHluW7YJCbSDkENzkioZpE/Qz9inx9Nd/DDT7C48+eRLucpMH3QiNlR1MbDClRlh8oxweSck/QEGoR+SuW5/Cvk79g3VWu/hk0+GytxIQrPuCk5U7fQHbnHqT2wB9CxapIYx/ia+UxS5MRO/X/JH02Cv7LmP//Z";
    //       ChartingHelper ch = new ChartingHelper();
    //       byte[] imageBytes = ch.GetChart(SeriesChartType.Column);
    //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
 
    //using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
    //{
    //    Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
    //    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
    //    document.Open();
    //    document.Add(image);
    //    document.Close();
    //    byte[] bytes = memoryStream.ToArray();
    //    memoryStream.Close();
 
    //    Response.Clear();
    //    Response.ContentType = "application/pdf";
    //    Response.AddHeader("Content-Disposition", "attachment; filename=Image.pdf");
    //    Response.ContentType = "application/pdf";
    //    Response.Buffer = true;
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.BinaryWrite(bytes);
    //    Response.End();


    //}
           //CBSN


            //var xml = File.ReadAllText(Server.MapPath("~/uploads/CBSN.xml"));
            //var str = XElement.Parse(xml);
            //IDataRepository<Beneficiario> _beneficiario;

            //IAWContext _context;
            //_context = new AWContext();
            //_beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);




            //foreach (var row in str.Elements("fila"))
            //{
            //    //Beneficiario Header
            //    Beneficiario BeneficiarioToAdd = new Beneficiario();
            //    Random rnd = new Random();
            //    int month = rnd.Next(1, 13);
            //    BeneficiarioToAdd.Nombre = row.Element("Nombre").Value;
            //    BeneficiarioToAdd.Apellido = row.Element("Apellido1").Value + " " + row.Element("Apellido2").Value;
            //    BeneficiarioToAdd.Sexo = row.Element("Sexo").Value == "H" ? "M" : "F";
            //    BeneficiarioToAdd.Edad = row.Element("Edad").Value == "0" ? row.Element("Edad").Value + "|" + month : row.Element("Edad").Value + "|0";
            //    BeneficiarioToAdd.Direccion = row.Element("Direccion").Value;
            //    if (bool.Parse(row.Element("Flag").Value))
            //    {
            //        BeneficiarioToAdd.Codigo = row.Element("Codigo").Value;
            //    }
            //    else
            //    {
            //        BeneficiarioToAdd.Codigo = "";
            //    }
            //    BeneficiarioToAdd.Dui = "0" + row.Element("DUI").Value + "-" + rnd.Next(0, 1);
            //    BeneficiarioToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioToAdd.ID_Programa = 10;

            //    //Beneficiario Adicional

            //    BeneficiarioAdicional BeneficiarioAdicionalToAdd = new BeneficiarioAdicional();
            //    BeneficiarioAdicionalToAdd.TieneRegistroNacimiento = true;
            //    BeneficiarioAdicionalToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioAdicionalToAdd.NombreEmergencia = "";
            //    BeneficiarioAdicionalToAdd.NumeroEmergencia = "";


            //    //Beneficiario Compromiso

            //    BeneficiarioCompromiso BeneficiarioCompromisoToAdd = new BeneficiarioCompromiso();
            //    BeneficiarioCompromisoToAdd.AceptaCompromiso = true;
            //    BeneficiarioCompromisoToAdd.ExistioProblema = false;
            //    BeneficiarioCompromisoToAdd.SeCongrega = false;
            //    BeneficiarioCompromisoToAdd.Comentario = "";
            //    BeneficiarioCompromisoToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioCompromisoToAdd.NombreIglesia = "";

            //    //Beneficiario Salud

            //    BeneficiarioSalud BeneficiarioSaludToAdd = new BeneficiarioSalud();
            //    BeneficiarioSaludToAdd.EstadoSalud = "Satisfactoria";
            //    BeneficiarioSaludToAdd.TieneTarjeta = bool.Parse(row.Element("Flag").Value);
            //    BeneficiarioSaludToAdd.Enfermedad = "";
            //    BeneficiarioSaludToAdd.Discapacidad = "";
            //    BeneficiarioSaludToAdd.FechaCurvaCrecimiento = GetRandomDate(new DateTime(2016, 1, 1), new DateTime(2016, 3, 1));
            //    BeneficiarioSaludToAdd.FechaInmunizacion = GetRandomDate(new DateTime(2016, 1, 1), new DateTime(2016, 3, 1));
            //    BeneficiarioSaludToAdd.CreadoPor = "ADMIN";

            //    BeneficiarioToAdd.BeneficiarioAdicional.Add(BeneficiarioAdicionalToAdd);
            //    BeneficiarioToAdd.BeneficiarioCompromiso.Add(BeneficiarioCompromisoToAdd);
            //    BeneficiarioToAdd.BeneficiarioSalud.Add(BeneficiarioSaludToAdd);


            //    _beneficiario.Add(BeneficiarioToAdd);


            //    var x = 0;

            //}
            //_context.SaveChanges();



           //CDIC


            //var xml = File.ReadAllText(Server.MapPath("~/uploads/CDIC.xml"));
            //var str = XElement.Parse(xml);
            //IDataRepository<Beneficiario> _beneficiario;

            //IAWContext _context;
            //_context = new AWContext();
            //_beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);




            //foreach (var row in str.Elements("fila"))
            //{
            //    //Beneficiario Header
            //    Beneficiario BeneficiarioToAdd = new Beneficiario();
            //    Random rnd = new Random();
            //    int month = rnd.Next(1, 13);
            //    BeneficiarioToAdd.Nombre = row.Element("Nombre").Value;
            //    BeneficiarioToAdd.Apellido = row.Element("Apellido1").Value + " " + row.Element("Apellido2").Value;
            //    BeneficiarioToAdd.Sexo = row.Element("Sexo").Value == "H" ? "M" : "F";
            //    BeneficiarioToAdd.Edad = row.Element("Edad").Value == "0" ? row.Element("Edad").Value + "|" + month : row.Element("Edad").Value + "|0";
            //    BeneficiarioToAdd.Direccion = row.Element("Direccion").Value;
            //    if (bool.Parse(row.Element("Flag").Value))
            //    {
            //        BeneficiarioToAdd.Codigo = row.Element("Codigo").Value;
            //    }
            //    else
            //    {
            //        BeneficiarioToAdd.Codigo = "";
            //    }
            //    BeneficiarioToAdd.Dui = "0" + row.Element("DUI").Value + "-" + rnd.Next(0, 1);
            //    BeneficiarioToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioToAdd.ID_Programa = 11;

            //    //Beneficiario Adicional

            //    BeneficiarioAdicional BeneficiarioAdicionalToAdd = new BeneficiarioAdicional();
            //    BeneficiarioAdicionalToAdd.TieneRegistroNacimiento = true;
            //    BeneficiarioAdicionalToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioAdicionalToAdd.NombreEmergencia = "";
            //    BeneficiarioAdicionalToAdd.NumeroEmergencia = "";


            //    //Beneficiario Compromiso

            //    BeneficiarioCompromiso BeneficiarioCompromisoToAdd = new BeneficiarioCompromiso();
            //    BeneficiarioCompromisoToAdd.AceptaCompromiso = true;
            //    BeneficiarioCompromisoToAdd.ExistioProblema = false;
            //    BeneficiarioCompromisoToAdd.SeCongrega = false;
            //    BeneficiarioCompromisoToAdd.Comentario = "";
            //    BeneficiarioCompromisoToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioCompromisoToAdd.NombreIglesia = "";



            //    BeneficiarioToAdd.BeneficiarioAdicional.Add(BeneficiarioAdicionalToAdd);
            //    BeneficiarioToAdd.BeneficiarioCompromiso.Add(BeneficiarioCompromisoToAdd);



            //    _beneficiario.Add(BeneficiarioToAdd);


            //    var x = 0;

            //}
            //_context.SaveChanges();


            //Primera Infancia


            //var xml = File.ReadAllText(Server.MapPath("~/uploads/PrimeraInfancia.xml"));
            //var str = XElement.Parse(xml);
            //IDataRepository<Beneficiario> _beneficiario;

            //IAWContext _context;
            //_context = new AWContext();
            //_beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);




            //foreach (var row in str.Elements("fila"))
            //{
            //    //Beneficiario Header
            //    Beneficiario BeneficiarioToAdd = new Beneficiario();
            //    Random rnd = new Random();
            //    int month = rnd.Next(1, 13); 
            //    BeneficiarioToAdd.Nombre = row.Element("Nombre").Value;
            //    BeneficiarioToAdd.Apellido = row.Element("Apellido1").Value +" "+ row.Element("Apellido2").Value;
            //    BeneficiarioToAdd.Sexo = row.Element("Sexo").Value == "H" ? "M":"F";
            //    BeneficiarioToAdd.Edad = row.Element("Edad").Value == "0" ? row.Element("Edad").Value + "|" + month : row.Element("Edad").Value + "|0";
            //    BeneficiarioToAdd.Direccion = row.Element("Direccion").Value;
            //    if (bool.Parse(row.Element("Flag").Value))
            //    {
            //        BeneficiarioToAdd.Codigo = row.Element("Codigo").Value;
            //    }
            //    else
            //    {
            //        BeneficiarioToAdd.Codigo = "";
            //    }
            //    BeneficiarioToAdd.Dui = "0"+row.Element("DUI").Value+"-"+rnd.Next(0,1);
            //    BeneficiarioToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioToAdd.ID_Programa = 21;

            //    //Beneficiario Adicional

            //    BeneficiarioAdicional BeneficiarioAdicionalToAdd = new BeneficiarioAdicional();
            //    BeneficiarioAdicionalToAdd.TieneRegistroNacimiento = true;
            //    BeneficiarioAdicionalToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioAdicionalToAdd.NombreEmergencia = "";
            //    BeneficiarioAdicionalToAdd.NumeroEmergencia = "";


            //    //Beneficiario Compromiso

            //    BeneficiarioCompromiso BeneficiarioCompromisoToAdd = new BeneficiarioCompromiso();
            //    BeneficiarioCompromisoToAdd.AceptaCompromiso = true;
            //    BeneficiarioCompromisoToAdd.ExistioProblema = false;
            //    BeneficiarioCompromisoToAdd.SeCongrega = false;
            //    BeneficiarioCompromisoToAdd.Comentario = "";
            //    BeneficiarioCompromisoToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioCompromisoToAdd.NombreIglesia = "";

            //    //Beneficiario Salud

            //    BeneficiarioSalud BeneficiarioSaludToAdd = new BeneficiarioSalud();
            //    BeneficiarioSaludToAdd.EstadoSalud = "Satisfactoria";
            //    BeneficiarioSaludToAdd.TieneTarjeta = bool.Parse(row.Element("Flag").Value);
            //    BeneficiarioSaludToAdd.Enfermedad = "";
            //    BeneficiarioSaludToAdd.Discapacidad = "";
            //    BeneficiarioSaludToAdd.FechaCurvaCrecimiento = GetRandomDate(new DateTime(2016, 1, 1), new DateTime(2016,3, 1));
            //    BeneficiarioSaludToAdd.FechaInmunizacion = GetRandomDate(new DateTime(2016, 1, 1), new DateTime(2016, 3, 1));
            //    BeneficiarioSaludToAdd.CreadoPor = "ADMIN";

            //    BeneficiarioToAdd.BeneficiarioAdicional.Add(BeneficiarioAdicionalToAdd);
            //    BeneficiarioToAdd.BeneficiarioCompromiso.Add(BeneficiarioCompromisoToAdd);
            //    BeneficiarioToAdd.BeneficiarioSalud.Add(BeneficiarioSaludToAdd);


            //    _beneficiario.Add(BeneficiarioToAdd);


            //    var x = 0;

            //}
            //_context.SaveChanges();

            //CAJ


            //var xml = File.ReadAllText(Server.MapPath("~/uploads/CAJ.xml"));
            //var str = XElement.Parse(xml);
            //IDataRepository<Beneficiario> _beneficiario;

            //IAWContext _context;
            //_context = new AWContext();
            //_beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);




            //foreach (var row in str.Elements("fila"))
            //{
            //    //Beneficiario Header
            //    Beneficiario BeneficiarioToAdd = new Beneficiario();
            //    Random rnd = new Random();
            //    int month = rnd.Next(1, 13);
            //    BeneficiarioToAdd.Nombre = row.Element("Nombre").Value;
            //    BeneficiarioToAdd.Apellido = row.Element("Apellido1").Value + " " + row.Element("Apellido2").Value;
            //    BeneficiarioToAdd.Sexo = row.Element("Sexo").Value == "H" ? "M" : "F";
            //    BeneficiarioToAdd.Edad = row.Element("Edad").Value == "0" ? row.Element("Edad").Value + "|" + month : row.Element("Edad").Value + "|0";
            //    BeneficiarioToAdd.Direccion = row.Element("Direccion").Value;
            //    if (bool.Parse(row.Element("Flag").Value))
            //    {
            //        BeneficiarioToAdd.Codigo = row.Element("Codigo").Value;
            //    }
            //    else
            //    {
            //        BeneficiarioToAdd.Codigo = "";
            //    }
            //    BeneficiarioToAdd.Dui = "0" + row.Element("DUI").Value + "-" + rnd.Next(0, 1);
            //    BeneficiarioToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioToAdd.ID_Programa = 13;

            //    //Beneficiario Adicional

            //    BeneficiarioAdicional BeneficiarioAdicionalToAdd = new BeneficiarioAdicional();
            //    BeneficiarioAdicionalToAdd.TieneRegistroNacimiento = true;
            //    BeneficiarioAdicionalToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioAdicionalToAdd.NombreEmergencia = "";
            //    BeneficiarioAdicionalToAdd.NumeroEmergencia = "";


            //    //Beneficiario Compromiso

            //    BeneficiarioCompromiso BeneficiarioCompromisoToAdd = new BeneficiarioCompromiso();
            //    BeneficiarioCompromisoToAdd.AceptaCompromiso = true;
            //    BeneficiarioCompromisoToAdd.ExistioProblema = false;
            //    BeneficiarioCompromisoToAdd.SeCongrega = false;
            //    BeneficiarioCompromisoToAdd.Comentario = "";
            //    BeneficiarioCompromisoToAdd.CreadoPor = "ADMIN";
            //    BeneficiarioCompromisoToAdd.NombreIglesia = "";



            //    BeneficiarioToAdd.BeneficiarioAdicional.Add(BeneficiarioAdicionalToAdd);
            //    BeneficiarioToAdd.BeneficiarioCompromiso.Add(BeneficiarioCompromisoToAdd);
                

            //    _beneficiario.Add(BeneficiarioToAdd);


            //    var x = 0;

            //}
            //_context.SaveChanges();


            //CIC


            var xml = File.ReadAllText(Server.MapPath("~/uploads/CIC.xml"));
            var str = XElement.Parse(xml);
            IDataRepository<Beneficiario> _beneficiario;

            IAWContext _context;
            _context = new AWContext();
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);




            foreach (var row in str.Elements("fila"))
            {
                //Beneficiario Header
                Beneficiario BeneficiarioToAdd = new Beneficiario();
                Random rnd = new Random();
                int month = rnd.Next(1, 13);
                BeneficiarioToAdd.Nombre = row.Element("Nombre").Value;
                BeneficiarioToAdd.Apellido = row.Element("Apellido1").Value + " " + row.Element("Apellido2").Value;
                BeneficiarioToAdd.Sexo = row.Element("Sexo").Value == "H" ? "M" : "F";
                BeneficiarioToAdd.Edad = row.Element("Edad").Value == "0" ? row.Element("Edad").Value + "|" + month : row.Element("Edad").Value + "|0";
                BeneficiarioToAdd.Direccion = row.Element("Direccion").Value;
                if (bool.Parse(row.Element("Flag").Value))
                {
                    BeneficiarioToAdd.Codigo = row.Element("Codigo").Value;
                }
                else
                {
                    BeneficiarioToAdd.Codigo = "";
                }
                BeneficiarioToAdd.Dui = "0" + row.Element("DUI").Value + "-" + rnd.Next(0, 1);
                BeneficiarioToAdd.CreadoPor = "ADMIN";
                BeneficiarioToAdd.ID_Programa = 12;

                //Beneficiario Adicional

                BeneficiarioAdicional BeneficiarioAdicionalToAdd = new BeneficiarioAdicional();
                BeneficiarioAdicionalToAdd.TieneRegistroNacimiento = true;
                BeneficiarioAdicionalToAdd.CreadoPor = "ADMIN";
                BeneficiarioAdicionalToAdd.NombreEmergencia = "";
                BeneficiarioAdicionalToAdd.NumeroEmergencia = "";


                //Beneficiario Compromiso

                BeneficiarioCompromiso BeneficiarioCompromisoToAdd = new BeneficiarioCompromiso();
                BeneficiarioCompromisoToAdd.AceptaCompromiso = true;
                BeneficiarioCompromisoToAdd.ExistioProblema = false;
                BeneficiarioCompromisoToAdd.SeCongrega = false;
                BeneficiarioCompromisoToAdd.Comentario = "";
                BeneficiarioCompromisoToAdd.CreadoPor = "ADMIN";
                BeneficiarioCompromisoToAdd.NombreIglesia = "";

                //Beneficiario Educación

                BeneficiarioEducacion BeneficiarioEducacionToAdd = new BeneficiarioEducacion();

                string[] Grados = GetCurrentAndPreviousYear(row.Element("Edad").Value);

                BeneficiarioEducacionToAdd.Estudia = true;
                BeneficiarioEducacionToAdd.GradoEducacion = Grados[0];
                BeneficiarioEducacionToAdd.Motivo = "";
                BeneficiarioEducacionToAdd.UltimoGrado = Grados[1];
                BeneficiarioEducacionToAdd.UltimoAño = "2015";
                BeneficiarioEducacionToAdd.NombreCentroEscolar = "";
                if (bool.Parse(row.Element("Flag").Value))
                {
                    BeneficiarioEducacionToAdd.Turno = "Mañana";
                }
                else
                {
                    BeneficiarioEducacionToAdd.Turno = "Tarde";
                }

                BeneficiarioToAdd.BeneficiarioAdicional.Add(BeneficiarioAdicionalToAdd);
                BeneficiarioToAdd.BeneficiarioCompromiso.Add(BeneficiarioCompromisoToAdd);
                BeneficiarioToAdd.BeneficiarioEducacion.Add(BeneficiarioEducacionToAdd);

                _beneficiario.Add(BeneficiarioToAdd);


            }
            _context.SaveChanges();

        }
        static readonly Random rnd = new Random();
        public static DateTime GetRandomDate(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }

        public string [] GetCurrentAndPreviousYear(string age)
        {
            string[] returnArray= new string [2];

            if (age=="7")
            {
                returnArray[0] = "2do Grado";
                returnArray[1] = "1er Grado";
            }
            if (age == "8")
            {
                returnArray[0] = "3er Grado";
                returnArray[1] = "2do Grado";
            }
            if (age == "9")
            {
                returnArray[0] = "4to Grado";
                returnArray[1] = "3er Grado";
            }
            if (age == "10")
            {
                returnArray[0] = "5to Grado";
                returnArray[1] = "4to Grado";
            }
            if (age == "11")
            {
                returnArray[0] = "6to Grado";
                returnArray[1] = "5to Grado";
            }
            if (age == "12")
            {
                returnArray[0] = "7mo Grado";
                returnArray[1] = "6to Grado";
            }

            return returnArray;
        }
    }
}