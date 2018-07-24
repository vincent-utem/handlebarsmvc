/*
 * handlebars玩家指南
 * https://cnodejs.org/topic/56a2e8b1cd415452622eed2d
 * 
 * 
*/


function hbController() {
    var curUrl = window.location.pathname, rnd = parseInt(Math.random() * 100000);
    var jsonUrl = curUrl.replace(/^\/wwwtpl/gi, '/wwwtpl/js/json').replace(/\.hbhtml$/gi, '') + '.json?' + rnd;
    $.getJSON(jsonUrl, function (jsonData) {
        $.each($("script[apply='hbtpl']"), function (i, tpl) {
            if ($($(tpl).text()).length == 1) {
                var template = Handlebars.compile($(tpl).html());
                $(tpl).replaceWith(template(jsonData));
            } else {
                $(tpl).replaceWith('<b>当前模板节点报错，错误原因：hbtpl模板节点下，必须且只有一个子节点。</b><xmp>' + $(tpl).get(0).outerHTML + '</xmp>');
            }
        });
    });
};

$(function () {
    hbController();
});