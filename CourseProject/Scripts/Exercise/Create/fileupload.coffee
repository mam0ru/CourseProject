jqXHRData = null

initFileUpload = () ->
  alert "initialize"
  $('#fileupload').fileupload({
    url: '/Exercise/UploadImage',
    dataType: 'json',
    add: (e, data) -> 
        alert "data add"
        jqXHRData = data
    done: (event, data) -> alert "asd"
    fail: (event, data) -> alert "qwe"})

$ ->
  $(document).ready ()->
    alert "ready"
    initFileUpload()
    alert "init"
    $('#Upload').on 'click', () ->
        if jqXHRData
          alert "submit"
          jqXHRData.submit()
        alert "false"
        return false
    alert "end"