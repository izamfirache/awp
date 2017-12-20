/* Utility functions */
function clearFormElementById(id) {
    document.getElementById(id).value = '';
}

function imageError(image) {
    image.onerror = "";
    image.src = "http://localhost/Content/images/fractal_wallpaper.png";
    return true;
}

/* Add Course Builder Topic */
function _constructCourseBuilderTopic() {
    var $topic = `
        <div class="course-builder-topic">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-md-2" for="Title">Title</label>
                    <div class="col-md-10">
                        <input class="form-control" name="TopicTitle" placeholder="Topic Title" type="text" value="">
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2" for="Description">Description</label>
                    <div class="col-md-10">
                        <textarea class="form-control" cols="20" name="TopicDescription" placeholder="Provide a small topic description" rows="2"></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2" for="Resource">Resource</label>
                    <div class="col-md-10">
                        <input class="form-control" name="TopicResourceName" placeholder="Resource name" type="text" value="">
                        <input class="form-control" name="TopicResourceURL" placeholder="Resource URL" type="text" value="">
                    </div>
                </div>
            </div>
        </div>
    `;

    return $topic;
}

function _isValidTopic($topic) {
    $topic.find('.has-error').removeClass('has-error');

    // Check the Topic title
    $topicTitle = $topic.find("input[name='TopicTitle']");
    if ($topicTitle.val().length == 0) {
        $topicTitle.closest('.form-group').addClass('has-error');
        return false;
    }

    // Check the Topic resource
    $topicResourceName = $topic.find("input[name='TopicResourceName']");
    $topicResourceURL = $topic.find("input[name='TopicResourceURL']");
    if ($topicResourceName.val().length == 0|| $topicResourceURL.val().length == 0) {
        $topicResourceName.closest('.form-group').addClass('has-error');
        return false;
    }

    return true;
}

function addCourseBuilderTopic() {
    var $topics = $('#course-builder-topics');

    // Validate existing topics so far
    var validTopics = true;
    $topics.children().each(function () {
        if (validTopics) {
            validTopics = _isValidTopic($(this));
        }
    });
    if (validTopics == false) {
        return;
    }

    // Construct and append a new topic
    $topics.append(_constructCourseBuilderTopic);

    // Show the 'Remove Topic' button
    if ($topics.children().length > 1) {
        $('#removeTopic').css('display', 'block');
    }
}

/* Remove Course Builder Topic */
function removeCourseBuilderTopic() {
    var $topics = $('#course-builder-topics').children();

    if ($topics.length > 1) {
        // Hide the 'Remove Topic' button if only one topic will be left
        if ($topics.length == 2) {
            $('#removeTopic').css('display', 'none');
        }

        $topics.last().remove();
    } else {
        alert('Cannot remove the only remaining course topics.');
    }
}


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

/* Trigger jQuery bar rating plugin */
$(document).ready(function () {
    $('#ratingScale').barrating({
        theme: 'bars-movie'
    });
});

/* Trigger Course Box image resizing */
function _courseBoxImageResize() {
    $('.course-box .course-image').each(function () {
        var parentWidth = $(this).parent().width();
        var height = parentWidth * 9 / 16;

        if (height > 0) {
            $(this).css('width', parentWidth);
            $(this).css('height', height); 
        }
    });
}

$(document).ready(function () {
    /* Resize images on page load */
    _courseBoxImageResize();
    /* Resize images every time the browser window is resized */
    $(window).resize(_courseBoxImageResize);
    /* Resized images on carousel controls (timeout ensures resizing will happen after control action) */
    $('.carousel-control').click(function () {
        setTimeout(_courseBoxImageResize, 0);
    });
});
