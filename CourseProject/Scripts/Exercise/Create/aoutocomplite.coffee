$ ->
  $(document).ready(
    $('#addTag').on 'click', (e) ->
      e.preventDefault()
      e.stopPropagation()
      parent = document.createElement('row')
      parent.className = 'row'
      child = document.createElement('input')
      child.type = 'text'
      child.name = "Tags"
      child.setAttribute('data-autocomplete-source', "/Exercise/TagAutocompliteSearch")
      child.setAttribute('data-val','true')
      child.setAttribute('data-val-required',"Требуется поле Tags.")
      parent.appendChild child
      $('#Tags').append parent
      $("[data-autocomplete-source]").each () ->
        target = $(this)
        target.autocomplete({ source: target.attr("data-autocomplete-source") })
  )
         
  $("[data-autocomplete-source]").each () ->
    target = $(this)
    target.autocomplete({ source: target.attr("data-autocomplete-source") })