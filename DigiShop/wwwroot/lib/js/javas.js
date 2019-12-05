function setSiteColor(color){
    document.documentElement.style.setProperty('--site-theme-color', color);
}


$(function () {
    $("div.star-rating-rtl > s").on("click", function (e) {

        // remove all active classes first, needed if user clicks multiple times
        $(this).closest('div').find('.active').removeClass('active');

        $(e.target).addClass('active');  // the element user has clicked on
        $(e.target).parentsUntil("div").addClass('active'); // all elements up from the clicked one excluding self

        var numStars = $(e.target).parentsUntil("div").length + 1;
        $(this).closest('div').find('.show-result').text(numStars + " ستاره");
    });
});
