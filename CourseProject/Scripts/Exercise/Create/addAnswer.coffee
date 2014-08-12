$ ->
  $(document).ready ()->
    $('#addAnswer').on 'click', (e)->
      e.stopPropagation()
      e.preventDefault()
      parent = document.createElement('row')
      parent.className = 'row'
      child = document.createElement('input')
      child.type = 'text'
      child.name = 'answer'
      parent.appendChild child
      $('#answer').append(parent)