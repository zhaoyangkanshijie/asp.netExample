namespace Models
{
    public class YOPModel
    {
        //商户编号,测试用
        public string merchantNo
        {
            get { return "BM12345678902702"; }
        }

        //商户私钥,测试用
        public string privatekey
        {
            get { return "MIIEogIBAAKCAQEAoyYAUC4jdra9jfb7/rsORFOz7WTxh164AuuwbwMWro1IgIw+RuMZ8iynl8wXIHQoYKW6KWMywJRIgqfPHb1rWeHMownTAc/5x2lr+7suqnO+Y3kun44gy18TXytPQNPpinQJdULgTQI2gZDcK/Rh143/ZEkAcf72QF19w+BGa7a9Ow1N6+/KuyKKxCGziEW0BWvzTFFK65J9ir8etgeN0PTxZ5WvordBi/Ua9m/H+Gf9II+CmOZNeSb+QiAhmHrbDXpyGGlqhMQIWNIOjOXXOGD4Fz2YtpQ0tMLVeIO/IOEcl6a3vNoLkrr0NKRqx7jKEYoH7UlJv35gYf/EnMeR7wIDAQABAoIBAC+D7s1rUprNiBFjoGrE8dfGhP2by5hAZtk66Wy7eoylyzrdHXopxDG6/aiBIS8rhWL4gWpyYKKjzSZ4VyLzOuO3lpDZWKILf1SriG6NSn8MLKPt9D01+KkibJLoGzHnRfoz51lGe+sRvOwpU2Zdy20rBvmfJUkOF4wRboJwvDG8VLkBydNx9acuyWuWxXKj8arlOCkpmrMDOrZ+xOBqQK5YemerAucyzRlGstvpkCy3avMXrbaWdoxZ2D3N/5RXhOLUrhEnksT3ZmG4FJVtgDEVGphT0c/lKudeoWLYfmDAnMaErj6IM/HfqDEZxKmbRWZXGQl4+B/r0+Yc5MG/g9ECgYEA2saztF+Q7Y6zvfVL49N6WwTTkAMUsO5QFX7ZHFoB3zTLvfXYvO2hJU5w48m+YE4kO+zKKbJxzPs/Fp+yjvMmwUko6P6KaNLpMK8l+Yxkr8S/VPLAYib2G0OapsHkTL2ResqSa1RlvHzLTbHDxQcpDYD8Z5poGN1cEQY7UN43fUkCgYEAvuhOPemdlBMlEI/Lgp7R3KVaZUQW7aWSKNMhm71d4T8oeuO1wF5JP0/2B72X7xJV//b90MaIEY+5u1oUOoJchT+EGebzFfeEEuPIB3ZRFvJflg9OOmMn+u9wkl+h6WRG4VHhqCwDPLJJzozUeOjyW18D2bGcRUxcHonhreDCrXcCgYBuaiKAQksu2fq1QHvQvAbgsQrlf+iNc3lPn/mLaZHQSSEa+l0s3PGbln87N7KxgD6hT9yoNrtgrN0mWesQYn+IxZe0H+NTDD9MptkPnV+jpjS6dtnJr8g98ly3FNxYLsShqGNFcA74ljM1PyaC5h4+Bn9c+nzXL8Erhm2hXsW6oQKBgHG+KTfcI+XgjjnS4tb9V15WKoihS+PlvTKTsLeA2RlmLvEhEN3/jzaoppawEIEBdLnf6BPm5ZVJA8krf1fo6cT+Ne/U2UpiQY+bpUdE6EV6vRbEIcDJ6T0qQfEEB4zuEQkYZxFyv67/LthgsgskB5oG/11J5CipuUz8q7iUbIk7AoGAGyLtB9I0lYouQT/NixxS0PdIUTr37MHm7neF0TE5mBfYzvfgFa4XOXunVbcC0HGgXpKdiB/5p7M7667Y/CuGZVl6Ymw6btdni5nzd0Koi0+X2LhXcjPvXXg75sxhYctJSW5szfxEIVlQUvYDxtyqdrC2efV8/77m6yVaZpwtD4E="; }
        }

        //易宝公钥，请勿修改
        public string yopPublicKey
        {
            get { return "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA6p0XWjscY+gsyqKRhw9MeLsEmhFdBRhT2emOck/F1Omw38ZWhJxh9kDfs5HzFJMrVozgU+SJFDONxs8UB0wMILKRmqfLcfClG9MyCNuJkkfm0HFQv1hRGdOvZPXj3Bckuwa7FrEXBRYUhK7vJ40afumspthmse6bs6mZxNn/mALZ2X07uznOrrc2rk41Y2HftduxZw6T4EmtWuN2x4CZ8gwSyPAW5ZzZJLQ6tZDojBK4GZTAGhnn3bg5bBsBlw2+FLkCQBuDsJVsFPiGh/b6K/+zGTvWyUcu+LUj2MejYQELDO3i2vQXVDk7lVi2/TcUYefvIcssnzsfCfjaorxsuwIDAQAB"; }
        }

        //订单号（唯一请求号）
        public string requestNo { get; set; }

        //商户用户唯一编号
        public string merchantUserId { get; set; }

        //订单金额
        public string orderAmount { get; set; }

        //需支付金额
        public string fundAmount { get; set; }

        //指定支付方式为网银个人支付
        public string payTool
        {
            get { return "SALESB2C"; }
        }

        //下单时间
        public string merchantOrderDate { get; set; }

        //指定银行编码
        public string bankCode { get; set; }

        //服务器异步通知地址
        public string serverCallbackUrl
        {
            get { return "https://www.***.com/Home/YOPNotifyReturn/"; }
        }

        //成功后返回页面地址
        public string webCallbackUrl
        {
            get { return "https://www.***.com/Home/YOPReturn/"; }
        }

        //商品所属行业类别
        public string mcc
        {
            get { return "3101"; }
        }

        //产品类别码(同mcc)
        public string productCatalog
        {
            get { return "3101"; }
        }

        //商品名称
        public string productName { get; set; }

        //商品描述
        public string productDesc { get; set; }

        //发起调用的服务器ip
        public string ip { get; set; }
    }
}