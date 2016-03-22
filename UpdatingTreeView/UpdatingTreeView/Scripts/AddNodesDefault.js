jQuery(function ($) {
    $('#rangeValue').on("change", function () {
        $('#rangeText').html(this.value);
    });
    $('#defaultRange').click(function () {
        $('#rangeValue').val('3');
        $('#rangeText').html($('#rangeValue').val());
        $('#LowerRandom').val('1');
        $('#UpperRandom').val('100');
    });

});