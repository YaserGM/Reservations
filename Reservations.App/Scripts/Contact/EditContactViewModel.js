var EditContactViewModel = {
    Id : ko.observable(),
    Name : ko.observable(),
    Birthdate: ko.observable(),
    ContactType: ko.observable(),
    Phone: ko.observable(),
    Description: ko.observable(),
    
    saveData : function () {
        //var delta = quill.getContents(); // to get the quills data

        //EditContactViewModel.Description(delta);
        //var json = ko.toJSON(EditContactViewModel);
        ////var fullJson = "input: " + json;
        //var test = json.toString();
        //$.post("/Contact/Edit", json).done(function (data) {
        //    alert("Data Loaded: " + data);
        //});

        var json = ko.toJS(EditContactViewModel);
        $.ajax({
            url: "/Contact/Edit",
            type: "POST",
            data: JSON.stringify(json),
            contentType: "application/json",
            success: function (result) {

            }
        });
    }
};

var quill = new Quill('#editor', {
    theme: 'snow',
    modules: {
        toolbar: [
            [
                { 'size': ['small', false, 'large', 'huge'] },
                'bold', 'italic', 'underline'
            ],
            [{ 'align': ['', 'center', 'right'] } ],
            [
                { list: 'bullet' },
                { list: 'ordered' }
            ],
            [ 'link', 'image', 'video']
        ]
    }
});

ko.applyBindings(EditContactViewModel);

function getIdFromUrl() {
    var location = window.location.href;
    var splitted = location.split('/');
    var result = splitted[splitted.length - 1];
    return result;
}

var id = getIdFromUrl();
//// preguntarle a alejo como obtener else else valor de la url del navegador pa poner en else id
$.getJSON("/Contact/GetById/" + id,
    function (data) {
            EditContactViewModel.Id(data.Id);
            EditContactViewModel.Name(data.Name);
            EditContactViewModel.Phone(data.Phone);
            EditContactViewModel.Birthdate(data.Birthdate);
            EditContactViewModel.ContactType(data.ContactType);
            EditContactViewModel.Description(data.Description);
        
    });



