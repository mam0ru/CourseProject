(function() {
  'use strict';
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
        alert("ERROR");
        if (data.files[0].error) {
          return alert(data.files[0].error);
        }
      }
    });
  };

  $(document).ready(function() {
    initFileUpload();
    return $('#Upload').on('click', function(e) {
      e.stopPropagation();
      if (jqXHRData) {
        alert("upload");
        jqXHRData.submit();
      }
      return false;
    });
  });

}).call(this);

//# sourceMappingURL=fileupload.js.map
