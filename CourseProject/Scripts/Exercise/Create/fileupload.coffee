jqXHRData = null

initFileUpload = () ->
  $('#imageupload').fileupload({
    url: '/Exercise/UploadImage',
    dataType: 'json',
    add: (e, data) -> 
        jqXHRData = data
    done: (event, data) -> 
        alert "file uploaded"
        jsItem = document.createElement('img')
        jsItem.src=data.result.path
        $('#uploaded_images').append(jsItem)
    fail: (event, data) ->
        alert "uplod filed"
        if data.files[0].error
          alert data.files[0].error
        })

$ ->
  $(document).ready ()->
    initFileUpload()
    $('#Upload').on 'click', () ->
        if jqXHRData
          jqXHRData.submit()
        return false