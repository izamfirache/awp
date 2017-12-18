/* Multi Item carousel */
$(document).ready(function () {
    $('.multi-item-carousel .item').each(function () {
        var itemToClone = $(this);

        for (var i = 1; i < 4; i++) {
            itemToClone = itemToClone.next();

            // wrap around if at end of item collection
            if (!itemToClone.length) {
                itemToClone = $(this).siblings(':first');
            }

            // Grab item, clone, add marker and add to collection
            itemToClone.children(':first-child').clone()
                .addClass('cloned-item-' + (i))
                .appendTo($(this));
        }
    });
});
