(function() {
  var initFileUpload, jqXHRData;

  jqXHRData = null;

  initFileUpload = function() {
    alert("initialize");
    return $('#fileupload').fileupload({
      url: '/Exercise/UploadImage',
      dataType: 'json',
      add: function(e, data) {
        alert("data add");
        return jqXHRData = data;
      },
      done: function(event, data) {
        return alert("asd");
      },
      fail: function(event, data) {
        return alert("qwe");
      }
    });
  };

  $(function() {
    return $(document).ready(function() {
      alert("ready");
      initFileUpload();
      alert("init");
      $('#Upload').on('click', function() {
        if (!jqXHRData) {
          alert("data");
        }
        if (jqXHRData) {
          alert("submit");
          jqXHRData.submit();
        }
        alert("false");
        return false;
      });
      return alert("end");
    });
  });

}).call(this);
