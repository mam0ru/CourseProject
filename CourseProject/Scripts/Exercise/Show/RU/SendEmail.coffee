$ ->
    $(document).ready () ->
        form = $("#email-form")
        form.submit () ->
            emailData = {
                SenderName: $("#sender-name").val(),
                SenderAddress: $("#sender-address").val(),
                Message: $("#message").val()
            }
            $.ajax({
                url: "/Home/SendMail",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                data: JSON.stringify(emailData),
                success: (response) ->
                    $("#message").val("")
                    alert(response)
                error: () ->
                    $("#message").val("")
                    alert("Ошибка отправки сообщения. Попробуйте снова.")
            })
            return false