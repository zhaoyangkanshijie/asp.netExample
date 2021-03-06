let data = {
    "福建": [
        "福州市","厦门市","莆田市","三明市","泉州市","漳州市","南平市","龙岩市","宁德市"
    ],
    "西藏": [
        "拉萨市","昌都","山南","日喀则","那曲","阿里","林芝"
    ],
    "贵州": [
        "贵阳市","六盘水市","遵义市","安顺市","铜仁","黔西南","毕节","黔东南","黔南"
    ],
    "湖北": [
        "武汉市","黄石市","十堰市","宜昌市","襄樊市","鄂州市","荆门市","孝感市","荆州市","黄冈市","咸宁市","随州市","恩施","仙桃市","潜江市","天门市","神农架林区"
    ],
    "湖南": [
        "长沙市","株洲市","湘潭市","衡阳市","邵阳市","岳阳市","常德市","张家界市","益阳市","郴州市","永州市","怀化市","娄底市","湘西"
    ],
    "广东": [
        "广州市","韶关市","深圳市","珠海市","汕头市","佛山市","江门市","湛江市","茂名市","肇庆市","惠州市","梅州市","汕尾市","河源市","阳江市","清远市","东莞市","中山市","潮州市","揭阳市","云浮市"
    ],
    "重庆": ["重庆市"],
    "澳门": ["澳门"],
    "香港": ["香港"],
    "安徽": [
        "合肥市","芜湖市","蚌埠市","淮南市","马鞍山市","淮北市","铜陵市","安庆市","黄山市","滁州市","阜阳市","宿州市","巢湖市","六安市","亳州市","池州市","宣城市"
    ],
    "四川": [
        "成都市","自贡市","攀枝花市","泸州市","德阳市","绵阳市","广元市","遂宁市","内江市","乐山市","南充市","眉山市","宜宾市","广安市","达州市","雅安市","巴中市","资阳市","阿坝","甘孜","凉山"
    ],
    "新疆": [
        "乌鲁木齐市","克拉玛依市","吐鲁番","哈密","昌吉","博尔塔拉","巴音郭楞","阿克苏","克孜勒苏","喀什","和田","伊犁哈萨克","塔城","阿勒泰","石河子市","阿拉尔市","图木舒克市","五家渠市"
    ],
    "江苏": [
        "南京市","无锡市","徐州市","常州市","苏州市","南通市","连云港市","淮安市","盐城市","扬州市","镇江市","泰州市","宿迁市"
    ],
    "天津": ["天津市"],
    "吉林": [
        "长春市","吉林市","四平市","辽源市","通化市","白山市","松原市","白城市","延边"
    ],
    "宁夏": [
        "银川市", "石嘴山市", "吴忠市", "固原市", "中卫市"
    ],
    "河北": [
        "石家庄市","唐山市","秦皇岛市","邯郸市","邢台市","保定市","张家口市","承德市","衡水市","廊坊市","沧州市"
    ],
    "河南": [
        "郑州市","开封市","洛阳市","平顶山市","安阳市","鹤壁市","新乡市","焦作市","濮阳市","许昌市","漯河市","三门峡市","南阳市","商丘市","信阳市","周口市","驻马店市","济源市"
    ],
    "广西": [
        "南宁市","柳州市","桂林市","梧州市","北海市","防城港市","钦州市","贵港市","玉林市","百色市","贺州市","河池市","来宾市","崇左市"
    ],
    "上海": ["上海市"],
    "海南": [
        "海口市","三亚市","五指山市","琼海市","儋州市","文昌市","万宁市","东方市","定安县","屯昌县","澄迈县","临高县","白沙","昌江","乐东","陵水","保亭","琼中","西沙群岛","南沙群岛","中沙群岛"
    ],
    "江西": [
        "南昌市","景德镇市","萍乡市","九江市","新余市","鹰潭市","赣州市","吉安市","宜春市","抚州市","上饶市"
    ],
    "云南": [
        "昆明市","曲靖市","玉溪市","保山市","昭通市","丽江市","思茅市","临沧市","楚雄","红河哈尼族","文山","西双版纳","大理","德宏","怒江","迪庆"
    ],
    "甘肃": [
        "兰州市","嘉峪关市","金昌市","白银市","天水市","武威市","张掖市","平凉市","酒泉市","庆阳市","定西市","陇南市","临夏","甘南"
    ],
    "山东": [
        "济南市","青岛市","淄博市","枣庄市","东营市","烟台市","潍坊市","济宁市","泰安市","威海市","日照市","莱芜市","临沂市","德州市","聊城市","滨州市","菏泽市"
    ],
    "陕西": [
        "西安市","铜川市","宝鸡市","咸阳市","渭南市","延安市","汉中市","榆林市","安康市","商洛市"
    ],
    "浙江": [
        "杭州市","宁波市","温州市","嘉兴市","湖州市","绍兴市","舟山市","衢州市","金华市","台州市","丽水市"
    ],
    "内蒙古": [
        "呼和浩特市","包头市","乌海市","赤峰市","通辽市","鄂尔多斯市","呼伦贝尔市","巴彦淖尔市","乌兰察布市","兴安盟","锡林郭勒盟","阿拉善盟"
    ],
    "青海": [
        "西宁市","海东","海北","黄南","海南","果洛","玉树","海西"
    ],
    "辽宁": [
        "沈阳市","大连市","鞍山市","抚顺市","本溪市","丹东市","锦州市","营口市","阜新市","辽阳市","盘锦市","铁岭市","朝阳市","葫芦岛市"
    ],
    "台湾": ["台湾"],
    "黑龙江": [
        "哈尔滨市","齐齐哈尔市","鸡西市","鹤岗市","双鸭山市","大庆市","伊春市","佳木斯市","七台河市","牡丹江市","黑河市","绥化市","大兴安岭"
    ],
    "山西": [
        "太原市","大同市","阳泉市","长治市","晋城市","朔州市","晋中市","运城市","忻州市","临汾市","吕梁市"
    ],
    "北京": ["北京市"]
}

let selectSwitch1 = false, selectSwitch2 = false, selectHasValue1 = false, selectHasValue2 = false, typeSwitch = false;
let provinceName = "", cityName = "", type = "无线覆盖";

let appendProvince = () => {
    //console.log("appendProvince");
    for (let item in data) {
        $(".FormExample-address-list").eq(0).append('<li class="FormExample-address-list-item">' + item + '</li>');
    }
}

let appendCity = (province) => {
    //console.log("province");
    $(".FormExample-address-list").eq(1).html("");
    for (let item in data[province]) {
        $(".FormExample-address-list").eq(1).append('<li class="FormExample-address-list-item">' + data[province][item] + '</li>');
    }
    $(".FormExample-address").eq(1).find(".FormExample-address-placeholder").css("color", "#333333");
    $(".FormExample-address").eq(1).find(".FormExample-address-placeholder").html(data[province][0]);
    $(".FormExample-address-list").eq(1).find(".FormExample-address-list-item").css("background-image", '');
    $(".FormExample-address-list").eq(1).find(".FormExample-address-list-item").eq(0).css("background-image", 'url("../../Areas/m/static/images/FormExample/tick.png")');
    $(".FormExample-address-list").eq(1).find(".FormExample-address-list-item").css("color", "#333333");
    $(".FormExample-address-list").eq(1).find(".FormExample-address-list-item").eq(0).css("color", "#00A8FF");
    cityName = data[province][0];
    selectHasValue2 = true;
}

let chooseProvinse = (province) => {
    //console.log(province);
    $(".FormExample-address").eq(0).find(".FormExample-address-placeholder").css("color", "#333333");
    $(".FormExample-address").eq(0).find(".FormExample-address-placeholder").html(province);
    provinceName = province;
    selectHasValue1 = true;
    appendCity(province);
    $(".FormExample-address-list").eq(1).find(".FormExample-address-list-item").on("click", function (e) {
        //console.log("click city option");
        //e.stopPropagation();
        $(".FormExample-address-list").eq(1).find(".FormExample-address-list-item").css("background-image", '');
        $(this).css("background-image", 'url("../../Areas/m/static/images/FormExample/tick.png")');
        $(".FormExample-address-list").eq(1).find(".FormExample-address-list-item").css("color", "#333333");
        $(this).css("color", "#00A8FF");
        chooseCity($(this).html());
    });
}

let chooseCity = (city) => {
    //console.log(city);
    $(".FormExample-address").eq(1).find(".FormExample-address-placeholder").css("color", "#333333");
    $(".FormExample-address").eq(1).find(".FormExample-address-placeholder").html(city);
}

let toggleOption = (index) => {
    //console.log("toggleOption");
    let selectSwitch = index == 0 ? selectSwitch1 : selectSwitch2;
    let selectHasValue = index == 0 ? selectHasValue1 : selectHasValue2;
    let word = index == 0 ? "省" : "市";
    if (selectSwitch) {
        $(".FormExample-address").eq(index).find(".FormExample-address-list").addClass("not-visible");
        $(".FormExample-address").eq(index).css("background-image", "url('../../Areas/m/static/images/FormExample/unfold.png')");
        if (selectHasValue) {
            $(".FormExample-address").eq(index).find(".FormExample-address-placeholder").css("color", "#333333");
        }
        else {
            $(".FormExample-address").eq(index).find(".FormExample-address-placeholder").html(word);
        }
    }
    else {
        $(".FormExample-address").eq(index).find(".FormExample-address-list").removeClass("not-visible");
        $(".FormExample-address").eq(index).css("background-image", "url('../../Areas/m/static/images/FormExample/fold.png')");
        if (selectHasValue) {
            $(".FormExample-address").eq(index).find(".FormExample-address-placeholder").css("color", "#00A8FF");
        }
        else {
            $(".FormExample-address").eq(index).find(".FormExample-address-placeholder").html("");
        }
    }
    selectSwitch = !selectSwitch;
    (index == 0) ? (selectSwitch1 = selectSwitch) : (selectSwitch2 = selectSwitch);
}

let toggleType = () => {
    if(typeSwitch){
        $(".FormExample-type-list").addClass("not-visible");
        $(".FormExample-type-placeholder").css("color", "#333333");
        $(".FormExample-type").css("background-image", "url('../../Areas/m/static/images/FormExample/unfold.png')");
    }
    else{
        $(".FormExample-type-list").removeClass("not-visible");
        $(".FormExample-type-placeholder").css("color","#00A8FF");
        $(".FormExample-type").css("background-image", "url('../../Areas/m/static/images/FormExample/fold.png')");
    }
    typeSwitch = !typeSwitch;
}

let chooseType = (index) => {
    $(".FormExample-type-list-item").css("color", "#333333");
    $(".FormExample-type-list-item").css("background-image", "none");
    $(".FormExample-type-list-item").eq(index).css("color", "#00A8FF");
    $(".FormExample-type-list-item").eq(index).css("background-image", "url('../../Areas/m/static/images/FormExample/tick.png')");
    type = $(".FormExample-type-list-item").eq(index).html();
    $(".FormExample-type-placeholder").html(type);
}

let removeError = () => {
    $(".FormExample-address").css("border", "2px solid #e6e6e6");
    $(".FormExample-area").css("border", "2px solid #e6e6e6");
    $(".FormExample-phone").css("border", "2px solid #e6e6e6");
}

let checkPhone = (phone) => {
    if ((/^1[34578]\d{9}$/.test(phone))) {
        return true;
    }
    else {
        return false;
    }
}

$(() => {
    appendProvince();

    $(".FormExample-address").on("click", function (e) {
        //console.log("click select");
        e.stopPropagation();
        removeError();
        let index = $(".FormExample-address").index($(this));
        if (index == 0) {
            selectSwitch2 = true;
            toggleOption(1);
            toggleOption(index);
        }
        else {
            if (!selectHasValue2) {
                $(".FormExample-address").eq(0).css("border", "1px solid #FF3B30");
            }
            else {
                toggleOption(index);
            }
            selectSwitch1 = true;
            toggleOption(0);
        }
    });

    $("body").on("click", function (e) {
        //console.log("click body");
        e.stopPropagation();
        selectSwitch1 = true;
        selectSwitch2 = true;
        toggleOption(0);
        toggleOption(1);
    });

    $(".FormExample-address").eq(0).find(".FormExample-address-list-item").on("click", function (e) {
        //console.log("click province option");
        //e.stopPropagation();
        $(".FormExample-address").eq(0).find(".FormExample-address-list-item").css("background-image", '');
        $(this).css("background-image", 'url("../../Areas/m/static/images/FormExample/tick.png")');
        $(".FormExample-address").eq(0).find(".FormExample-address-list-item").css("color", "#333333");
        $(this).css("color", "#00A8FF");
        chooseProvinse($(this).html());
    });

    $(".FormExample-type").on("click", function(){
        toggleType();
    });

    $(".FormExample-type-list-item").on("click",function(){
        let index = $(".FormExample-type-list-item").index($(this));
        chooseType(index);
    });

    $(".FormExample-submit").on("click", function () {
        removeError();

        let scene = $(".FormExample-selection").html();
        //let provinceName = $(".FormExample-form-container-address-box-select-placeholder").eq(0).data("province");
        //let cityName = $(".FormExample-form-container-address-box-select-placeholder").eq(1).data("city");
        let area = parseInt($(".FormExample-area-input").val());
        //let type = $(".FormExample-form-container-type-box").data("type");
        let phone = $(".FormExample-phone").val();

        let flag = true;

        if (provinceName == "") {
            $(".FormExample-address").eq(0).css("border", "2px solid #FF3B30");
            flag = false;
        }
        if (cityName == "") {
            $(".FormExample-address").eq(1).css("border", "2px solid #FF3B30");
            flag = false;
        }
        if (area == "" || isNaN(area)) {
            $(".FormExample-area").css("border", "2px solid #FF3B30");
            flag = false;
        }
        if (type == "") {
            $(".FormExample-type").css("border", "2px solid #FF3B30");
            flag = false;
        }
        if (!checkPhone(phone)) {
            $(".FormExample-phone").css("border", "2px solid #FF3B30");
            flag = false;
        }

        //console.log(scene);
        //console.log(provinceName);
        //console.log(cityName);
        //console.log(area);
        //console.log(type);
        //console.log(phone);

        if (flag) {
            let jsonData = '{"action":"save","category":"' + scene + '","city":"' + provinceName + '/' + cityName + '","area1":"' + area + '","FormExample":"' + type + '","phone":"' + phone + '"}';
            //console.log(jsonData);
            $.ajax({
                url: '/m/home/FormSubmit/',
                type: 'post',
                processData: false,
                contentType: "application/json",
                data: jsonData,
                success: function (result) {
                    alert(result);
                },
                error: function (result) {
                    //console.log(result);
                }
            });
        }
    });
});