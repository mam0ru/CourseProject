$ ->
  $(document).ready () ->
    $(".md-header").css('height', '35px')
    $("[name='deleteTag']").on 'click', (e) ->
      e.stopPropagation()
      e.preventDefault()
      parent1 = $(this).parent()
      parent2 = parent1.parent()
      parent2.remove()