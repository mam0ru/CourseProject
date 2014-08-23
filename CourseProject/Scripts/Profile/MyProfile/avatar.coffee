$ ->
    $(document).ready () ->
        $('#changeAvatar').fileupload({
        url: '/profile/SetAvatar',
        dataType: 'json',
        add: (e, data) -> 
            $('.progress')[0].hidden = false
            $jqXHRData = data
            $jqXHRData.submit()
        done: (event, data) -> 
            $('.progress > .progress-bar').css('width',0 + '%')
            $('#avatar')[0].src = data.result.path
            $('.progress')[0].hidden = true       
        fail: (event, data) ->
            alert "ERROR"
            if data.files[0].error
                alert data.files[0].error
        progressall: (e, data) ->
            progress = parseInt(data.loaded / data.total * 100, 10)
            $('.progress > .progress-bar').css('width',progress + '%')})    