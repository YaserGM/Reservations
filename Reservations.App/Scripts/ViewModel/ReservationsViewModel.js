var ReservationsViewModel = function(model) {
    var self = this;

    self.reservations = ko.observableArray(model);

    self.filterReservation = function(page, sort) {

        $.ajax({
            type: 'POST',
            url: '/Reservations/FilterIndex',
            dataType: 'json',
            data: { page: page, sort: sort },
            success: function(data) {

                var parsed = JSON.parse(data);
                self.reservations(parsed);

                $('.rating').rating('refresh', null);
            },
            error: function(ex) {
                var a = ex;
            }
        });

    }


    self.changeFavorite = function(data) {

        for (var i = 0; i < self.reservations().length; i++) {
            if (self.reservations()[i].Id === data.Id) {
                self.reservations()[i].Favorite = self.reservations()[i].Favorite ? false : true;
                data.Favorite = self.reservations()[i].Favorite;
                $('#label-' + data.Id)
                    .removeClass(data.Favorite ? "label-enable" : "label-disable")
                    .addClass(data.Favorite ? "label-disable" : "label-enable");

                $('#img-' + data.Id)
                    .removeClass(data.Favorite ? "img-favorite-disable" : "img-favorite-enable")
                    .addClass(data.Favorite ? "img-favorite-enable" : "img-favorite-disable");
            }
        }

        $.ajax({
            type: 'POST',
            url: '/Reservations/UpdateFavorite',
            dataType: 'json',
            data: { id: data.Id, value: data.Favorite},
            success: function (data) {

            },
            error: function (ex) {
                var a = ex;
            }
        });

    }

}

redirectToUrl = function(base, id) {
    window.location = base + "&Id=" + id;
}


//call in function starClick by file "start-rating.js"
function updateRating(impId, startValue) {
    var compId = parseInt(impId.split("-")[1]);

    $.ajax({
        type: 'POST',
        url: '/Reservations/UpdateRating',
        dataType: 'json',
        data: { id: compId, value: startValue },
        success: function(stops) {
        },
        error: function(ex) {
        }
    });
}

$(document).ready(
    $("#drowfiltre").change(function () {
        if ($("#drowfiltre").val() !== "0") {
            var page = $("#form-sort").get(0).action.split("page=")[1].split("&")[0];
            var sort = $("#drowfiltre").val();
            viewModel.filterReservation(parseInt(page), sort);
        }
    })
);