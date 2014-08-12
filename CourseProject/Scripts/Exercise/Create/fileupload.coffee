'use strict'
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
        jsItem.width = 400
        jsItem.vspace = 5
        $('#uploaded_images').append(jsItem)
    fail: (event, data) ->
        alert "ERROR"
        if data.files[0].error
          alert data.files[0].error
  })

$(document).ready ()->
  initFileUpload()
  $('#Upload').on 'click', (e) ->
      e.stopPropagation()
      e.preventDefault()
      if jqXHRData
        alert "upload"
        jqXHRData.submit()
      return false