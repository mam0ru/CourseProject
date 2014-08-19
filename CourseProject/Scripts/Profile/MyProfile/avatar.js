(function() {
  $(function() {
    return $(document).ready(function() {
      return $('#changeAvatar').fileupload({
        url: '/profile/SetAvatar',
        dataType: 'json',
        autoUpload: true,
        add: function(e, data) {
          var $jqXHRData;
          $jqXHRData = data;
          return $jqXHRData.submit();
        },
        done: function(event, data) {
          $('.progress > .progress-bar').css('width', 0 + '%');
          return $('#avatar')[0].src = data.result.path;
        },
        fail: function(event, data) {
          alert("ERROR");
          if (data.files[0].error) {
            return alert(data.files[0].error);
          }
        },
        progressall: function(e, data) {
          var progress;
          progress = parseInt(data.loaded / data.total * 100, 10);
          return $('.progress > .progress-bar').css('width', progress + '%');
        }
      });
    });
  });

}).call(this);

//# sourceMappingURL=avatar.js.map
