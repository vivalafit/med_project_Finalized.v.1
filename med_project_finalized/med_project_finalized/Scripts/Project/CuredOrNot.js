$('a').css('color', function (index, oldValue) {
    if (oldValue == 'rgb(0, 0, 238)')
    { return 'red'; }
    else
    { return 'green'; }
});