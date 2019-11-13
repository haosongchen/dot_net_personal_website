$(document).ready(function () {

  


    $(".nav a").click(function () {

       

        goTo = $(this).attr("href");
    


        $("body, html").animate({ scrollTop: $(goTo).offset().top }, 1000);
    });

 


});

var egg = new Egg();
egg.addCode("m,s,c", function () {
    jQuery('#egggif').fadeIn(500, function () {
        window.setTimeout(function () { jQuery('#egggif').hide(); }, 5000);
    });
}, "konami-code");
egg.addHook(function () {
    console.log("Hook called for: " + this.activeEgg.keys);
    console.log(this.activeEgg.metadata);
});
egg.listen();