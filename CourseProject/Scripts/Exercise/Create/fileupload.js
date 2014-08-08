(function() {
  var initFileUpload, jqXHRData;

  jqXHRData = null;

  initFileUpload = function() {
    return $('#imageupload').fileupload({
      url: '/Exercise/UploadImage',
      dataType: 'json',
      add: function(e, data) {
        return jqXHRData = data;
      },
      done: function(event, data) {
        var jsItem;
        alert("file uploaded");
        jsItem = document.createElement('img');
        jsItem.src = data.result.path;
        jsItem.width = 400;
        jsItem.vspace = 5;
        return $('#uploaded_images').append(jsItem);
      },
      fail: function(event, data) {
        alert("uplod filed");
        if (data.files[0].error) {
          return alert(data.files[0].error);
        }
      }
    });
  };

  $(function() {
    return $(document).ready(function() {
      initFileUpload();
      return $('#Upload').on('click', function() {
        if (jqXHRData) {
          jqXHRData.submit();
        }
        return false;
      });
    });
  });

}).call(this);
