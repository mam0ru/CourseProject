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
            return alert("Ошибка отправки сообщения. Попробуйте снова.");
          }
        });
        return false;
      });
    });
  });

}).call(this);

//# sourceMappingURL=SendEmail.js.map
