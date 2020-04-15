

const _defaultImage = 'https://kingsteigntonschool.org/wp-content/uploads/2015/07/facebook-default-no-profile-pic.jpg';
var _friends = [
    { UserId: 1, Name: 'Brock Allen', ProfileImage: _defaultImage, RequestSent: false, icon: '"fa fa-check"' },
    { UserId: 2, Name: 'Jeffrey Fritz', ProfileImage: _defaultImage, RequestSent: true, icon: '"fa fa-user-plus"' },
    { UserId: 3, Name: 'Elena Scott', ProfileImage: _defaultImage, RequestSent: false, icon: '"fa fa-check"'},
    { UserId: 4, Name: 'James Han', ProfileImage: _defaultImage, RequestSent: false, icon: '"fa fa-user-plus"' }];

var _suggestions = $('#suggestions');

$(document).ready(function (e) {
    loadUser(_friends);
});

function loadUser(users) {
    users.forEach(function (v) {
        _suggestions.append('<li class="list-group-item">'
            + '<img src = "https://kingsteigntonschool.org/wp-content/uploads/2015/07/facebook-default-no-profile-pic.jpg" height = "50" class="mr-2" alt = "Profile image">'
            + '<span>' + v.Name + '</span>'
            + '<button type="button" class="btn btn-info btn-sm pull-right" onclick="addFriend(' + v.UserId + ')"><i class=' + v.icon + '></i> ' + ((v.RequestSent) ? 'Add Friend' : 'Request Sent') + '</button>'
            + '</li>');
    }); 
}

function addFriend(userId) {
    _suggestions.empty();

    _friends.forEach(function (v) {
        if (v.UserId === userId && v.RequestSent == false) {
            v.RequestSent = true;
        }
    });
    console.log('user' + JSON.stringify(_friends));
    loadUser(_friends);
}