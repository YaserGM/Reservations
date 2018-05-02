var ReservationViewModel = function(model) {
    var self = this;

    self.contactName = ko.observable(model.Contact.Name);
    self.phonenumber = ko.observable(model.Contact.PhoneNumber);
    self.contactTypeId = ko.observable(model.Contact.ContactTypeId);
    self.contacId = ko.observable(model.Contact.Id);
    self.description = ko.observable(model.Descriptions);

    if (model.Id > 0) {
        self.birthdate = ko.observable(dateTextToString(model.Contact.Birthdate));
    } else {
        self.birthdate = ko.observable();
    }

    initValues(model.Descriptions);

    self.activeSend = ko.computed(function() {

            // var contact = self.contactName();
            //  var phone = self.phonenumber();

            //  var active = ((contact !== undefined && contact !== "") && (phone !== undefined && phone !== null));

            // return self.contactTypeId() > 0;

            return true;
        },
        this).extend({ deferred: true });


    self.getByName = function(nameVal) {
        if (nameVal !== undefined && nameVal !== null && nameVal.trim() !== "") {

            $.ajax({
                type: "POST",
                url: "/Contacts/GetByName",
                dataType: 'json',
                data: { name: nameVal },
                success: function(dataResult) {
                    var data = JSON.parse(dataResult);

                    if (data.Name !== null) {
                        self.contactName(data.Name);
                    }
                    self.phonenumber(data.PhoneNumber);
                    self.birthdate(dateTextToString(data.Birthdate));
                    self.contactTypeId(data.ContactTypeId);
                    self.contacId(data.Id);
                },
                error: function(ex) {
                }
            });

        } else {
            self.contactName(undefined);
            self.phonenumber(undefined);
            self.birthdate(undefined);
            self.contactTypeId(undefined);
            self.contacId(undefined);
        }

    }

    self.sendResevation = function(formElement) {
        viewModel.description(quill.getText());

        $(formElement).validate();
        if (!$(formElement).valid()) {
            return false;
        }
        return true;
    }

}


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
                ['link', 'image']
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


function dateToString(date) {
    var monthReal = date.getMonth() + 1;
    var month = (monthReal > 9 ? monthReal : ("0" + monthReal));
    var day = (date.getDate() > 9 ? date.getDate() : ("0" + date.getDate()));

    var dateText = [date.getFullYear(), month, day].join("-");
    return dateText;
}

function sqlDateToString(sqlDate) {
    var date = new Date(parseInt(sqlDate.substr(6)));
    return dateToString(date);
}

function dateTextToString(dateText) {
    return dateToString(new Date(dateText));
}

function initValues(text) {
    if (text !== undefined && text !== null) {
        quill.setText(text);
    }
}

$(document).ready(
    $("#imp-contact").donetyping(function() {
        var name = $("#imp-contact").val();
        viewModel.getByName(name);
    }),
    $("#imp-contact").change(function() {
        var name = $("#imp-contact").val();
        viewModel.getByName(name);
    })
);