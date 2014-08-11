$ ->
  $(document).ready(
    $('#addTag').on 'click', (e) ->
      e.preventDefault()
      e.stopPropagation()
      parent = document.createElement('row')
      parent.className = 'row'
      child = document.createElement('input')
      child.type = 'text'
      child.name = "tag"
      child.setAttribute('data-autocomplete-source', "/Exercise/TagAutocompliteSearch")
      parent.appendChild child
      $('#Tags').append parent
      $("[data-autocomplete-source]").each () ->
        target = $(this)
        target.autocomplete({ source: target.attr("data-autocomplete-source") })
  )
         
  $("[data-autocomplete-source]").each () ->
    target = $(this)
    target.autocomplete({ source: target.attr("data-autocomplete-source") })