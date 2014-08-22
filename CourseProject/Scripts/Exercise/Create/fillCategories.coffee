$(document).ready () ->
  data = $.getJSON("/Exercise/GetCategoties", (data, textStatus, jqXHR) ->
    $.each data, (key, val) ->
      item = document.createElement 'option'
      item.text = val
      item.style.color = "black"
      $('#Category').append(item)
  )