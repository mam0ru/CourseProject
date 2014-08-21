(function() {
  $(function() {
    return $(document).ready(function() {
      var form;
      form = $("#email-form");
      return form.submit(function() {
        var emailData;
        emailData = {
          SenderName: $("#sender-name").val(),
          SenderAddress: $("#sender-address").val(),
          Message: $("#message").val()
        };
        $.ajax({
          url: "/Home/SendMail",
          type: "POST",
          contentType: "application/json; charset=utf-8",
          dataType: "text",
          data: JSON.stringify(emailData),
          success: function(response) {
            $("#message").val("");
            return alert(response);
          },
          error: function() {
            $("#message").val("");
            return alert("There was an error... please try again.");
          }
        });
        return false;
      });
    });
  });

}).call(this);

//# sourceMappingURL=SendEmail.js.map
