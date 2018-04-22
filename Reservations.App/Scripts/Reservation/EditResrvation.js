
var quill = new Quill('#editor',
    {
        theme: 'snow',
        modules: {
            toolbar: [
                [
                    { 'size': ['small', false, 'large', 'huge'] },
                    'bold', 'italic', 'underline'
                ],
                [{ 'align': ['', 'center', 'right'] }],
                [
                    { list: 'bullet' },
                    { list: 'ordered' }
                ],
                ['link', 'image', 'video']
            ]
        }
    });


(function($) {
    $.fn.extend({
        donetyping: function(callback, timeout) {
            timeout = timeout || 1e3; // 1 second default timeout
            var timeoutReference,
                doneTyping = function(el) {
                    if (!timeoutReference) return;
                    timeoutReference = null;
                    callback.call(el);
                };
            return this.each(function(i, el) {
                var $el = $(el);
                // Chrome Fix (Use keyup over keypress to detect backspace)
                // thank you @palerdot
                $el.is(':input') &&
                    $el.on('keyup keypress paste',
                        function(e) {
                            // This catches the backspace button in chrome, but also prevents
                            // the event from triggering too preemptively. Without this line,
                            // using tab/shift+tab will make the focused element fire the callback.
                            if (e.type == 'keyup' && e.keyCode != 8) return;

                            // Check if timeout has been set. If it has, "reset" the clock and
                            // start over again.
                            if (timeoutReference) clearTimeout(timeoutReference);
                            timeoutReference = setTimeout(function() {
                                    // if we made it here, our timeout has elapsed. Fire the
                                    // callback
                                    doneTyping(el);
                                },
                                timeout);
                        }).on('blur',
                        function() {
                            // If we can, fire the event since we're leaving the field
                            doneTyping(el);
                        });
            });
        }
    });
})(jQuery);


function initField() {
    quill.setText($('#_description').val());
    if ($('#_Id').val() === 0) {
        $('#imp-birthdate').val(null);
    }
}

$(document).ready(
    initField(),
    $('#btn-send').on('click',
        function() {
            var text = quill.getText();
            $('#_description').val(text);
        }),
    $("#imp-contact").donetyping(function() {
            var tt = $("#imp-contact").val();
            $.ajax({
                type: 'POST',
                url: "/Contacts/GetByName",
                dataType: 'json',
                data: { name: $("#imp-contact").val() },
                success: function(data) {
                    if (data.Name != null) {
                        $('#imp-contact').val(data.Name);
                    }
                    $('#phone-number').val(data.PhoneNumber);
                    $('#imp-birthdate').val(data.BirthdateText);
                    $('#drow-contactType').val(data.ContactType);
                    $('#_Id').val(data.Id);
                },
                error: function(ex) {
                }
            });
        }
    )
);