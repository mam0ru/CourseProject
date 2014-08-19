$ ->
    $(document).ready () ->
        $('#changeAvatar').fileupload({
        url: '/profile/SetAvatar',
        dataType: 'json',
        autoUpload: true,
        add: (e, data) -> 
            $jqXHRData = data
            $jqXHRData.submit()
        done: (event, data) -> 
            $('.progress > .progress-bar').css('width',0 + '%')
            $('#avatar')[0].src = data.result.path;       
        fail: (event, data) ->
            alert "ERROR"
            if data.files[0].error
                alert data.files[0].error
        progressall: (e, data) ->
            progress = parseInt(data.loaded / data.total * 100, 10)
            $('.progress > .progress-bar').css(
                'width',
                progress + '%')
          })    